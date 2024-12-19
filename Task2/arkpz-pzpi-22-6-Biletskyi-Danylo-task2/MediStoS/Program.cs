using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Repository.BatchRepository;
using MediStoS.Database.Repository.MedicineRepository;
using MediStoS.Database.Repository.SensorRepository;
using MediStoS.Database.Repository.StorageViolationRepository;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.Database.Repository.WarehouseRepository;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MediStoS", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapSwagger();
app.MapControllers();

app.Run();
