﻿// <auto-generated />
using System;
using MediStoS.Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediStoS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241226000528_SensorFix")]
    partial class SensorFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MediStoS.Database.Models.Batch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BatchNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("batch_number")
                        .HasAnnotation("Relational:JsonPropertyName", "batch_number");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expiration_date")
                        .HasAnnotation("Relational:JsonPropertyName", "expiration_date");

                    b.Property<DateTime>("ManufactureDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("manufacture_date")
                        .HasAnnotation("Relational:JsonPropertyName", "manufacture_date");

                    b.Property<int>("MedicineId")
                        .HasColumnType("integer")
                        .HasColumnName("medicine_id")
                        .HasAnnotation("Relational:JsonPropertyName", "medicine_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity")
                        .HasAnnotation("Relational:JsonPropertyName", "quantity");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id")
                        .HasAnnotation("Relational:JsonPropertyName", "user_id");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("integer")
                        .HasColumnName("warehouse_id")
                        .HasAnnotation("Relational:JsonPropertyName", "warehouse_id");

                    b.HasKey("Id")
                        .HasName("pk_batches");

                    b.HasIndex("MedicineId");

                    b.HasIndex("UserId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("batches", (string)null);
                });

            modelBuilder.Entity("MediStoS.Database.Models.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("manufacturer")
                        .HasAnnotation("Relational:JsonPropertyName", "manufacturer");

                    b.Property<float>("MaxHumidity")
                        .HasColumnType("real")
                        .HasColumnName("max_humidity")
                        .HasAnnotation("Relational:JsonPropertyName", "max_humidity");

                    b.Property<float>("MaxTemperature")
                        .HasColumnType("real")
                        .HasColumnName("max_temperature")
                        .HasAnnotation("Relational:JsonPropertyName", "max_temperature");

                    b.Property<float>("MinHumidity")
                        .HasColumnType("real")
                        .HasColumnName("min_humidity")
                        .HasAnnotation("Relational:JsonPropertyName", "min_humidity");

                    b.Property<float>("MinTemperature")
                        .HasColumnType("real")
                        .HasColumnName("min_temperature")
                        .HasAnnotation("Relational:JsonPropertyName", "min_temperature");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id")
                        .HasName("pk_medicines");

                    b.ToTable("medicines", (string)null);
                });

            modelBuilder.Entity("MediStoS.Database.Models.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("serial_number")
                        .HasAnnotation("Relational:JsonPropertyName", "serial_number");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type")
                        .HasAnnotation("Relational:JsonPropertyName", "type");

                    b.Property<float>("Value")
                        .HasColumnType("real")
                        .HasColumnName("value")
                        .HasAnnotation("Relational:JsonPropertyName", "value");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("integer")
                        .HasColumnName("warehouse_id")
                        .HasAnnotation("Relational:JsonPropertyName", "warehouse_id");

                    b.HasKey("Id")
                        .HasName("pk_sensors");

                    b.HasIndex("WarehouseId");

                    b.ToTable("sensors", (string)null);
                });

            modelBuilder.Entity("MediStoS.Database.Models.StorageViolation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("Humidity")
                        .HasColumnType("real")
                        .HasColumnName("humidity")
                        .HasAnnotation("Relational:JsonPropertyName", "humidity");

                    b.Property<DateTime>("RecordedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("recorded_at")
                        .HasAnnotation("Relational:JsonPropertyName", "recorded_at");

                    b.Property<float>("Temperature")
                        .HasColumnType("real")
                        .HasColumnName("temperature")
                        .HasAnnotation("Relational:JsonPropertyName", "temperature");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("integer")
                        .HasColumnName("warehouse_id")
                        .HasAnnotation("Relational:JsonPropertyName", "warehouse_id");

                    b.HasKey("Id")
                        .HasName("pk_storage_violations");

                    b.HasIndex("WarehouseId");

                    b.ToTable("storage_violations", (string)null);
                });

            modelBuilder.Entity("MediStoS.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email")
                        .HasAnnotation("Relational:JsonPropertyName", "email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name")
                        .HasAnnotation("Relational:JsonPropertyName", "first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name")
                        .HasAnnotation("Relational:JsonPropertyName", "last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password")
                        .HasAnnotation("Relational:JsonPropertyName", "password");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role")
                        .HasAnnotation("Relational:JsonPropertyName", "role");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MediStoS.Database.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address")
                        .HasAnnotation("Relational:JsonPropertyName", "address");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasAnnotation("Relational:JsonPropertyName", "created_at");

                    b.Property<float>("MaxHumidity")
                        .HasColumnType("real")
                        .HasColumnName("max_humidity")
                        .HasAnnotation("Relational:JsonPropertyName", "max_humidity");

                    b.Property<float>("MaxTemperature")
                        .HasColumnType("real")
                        .HasColumnName("max_temperature")
                        .HasAnnotation("Relational:JsonPropertyName", "max_temperature");

                    b.Property<float>("MinHumidity")
                        .HasColumnType("real")
                        .HasColumnName("min_humidity")
                        .HasAnnotation("Relational:JsonPropertyName", "min_humidity");

                    b.Property<float>("MinTemperature")
                        .HasColumnType("real")
                        .HasColumnName("min_temperature")
                        .HasAnnotation("Relational:JsonPropertyName", "min_temperature");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id")
                        .HasName("pk_warehouses");

                    b.ToTable("warehouses", (string)null);
                });

            modelBuilder.Entity("MediStoS.Database.Models.Batch", b =>
                {
                    b.HasOne("MediStoS.Database.Models.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_batches_medicines_medicine_id");

                    b.HasOne("MediStoS.Database.Models.User", "User")
                        .WithMany("Batches")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_batches_users_user_id");

                    b.HasOne("MediStoS.Database.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_batches_warehouses_warehouse_id");

                    b.Navigation("Medicine");

                    b.Navigation("User");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MediStoS.Database.Models.Sensor", b =>
                {
                    b.HasOne("MediStoS.Database.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sensors_warehouses_warehouse_id");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MediStoS.Database.Models.StorageViolation", b =>
                {
                    b.HasOne("MediStoS.Database.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_storage_violations_warehouses_warehouse_id");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MediStoS.Database.Models.User", b =>
                {
                    b.Navigation("Batches");
                });
#pragma warning restore 612, 618
        }
    }
}
