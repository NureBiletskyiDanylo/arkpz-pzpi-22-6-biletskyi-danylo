using System.Text;
using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Repository.BatchRepository;
using MediStoS.Database.Repository.MedicineRepository;
using MediStoS.Database.Repository.SensorRepository;
using MediStoS.Database.Repository.StorageViolationRepository;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.Database.Repository.WarehouseRepository;
using MediStoS.Services.SMTPService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Logging.AddConsole().AddDebug();
// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IStorageViolationRepository, StorageViolationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Secret"))),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MediStoS", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the token into header:"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string [] {}
        }
    });
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(new JsonFormatter(), "logs/logs.json", rollingInterval: RollingInterval.Month).CreateLogger();

AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapSwagger();
app.MapControllers();
app.Run();
