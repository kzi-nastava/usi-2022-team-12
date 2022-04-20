﻿// <auto-generated />
using System;
using HealthInstitution.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthInstitution.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("HealthInstitution.Model.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActivityType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfAction")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("activities");
                });

            modelBuilder.Entity("HealthInstitution.Model.Anamnesis", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("anamneses");
                });

            modelBuilder.Entity("HealthInstitution.Model.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AnamnesisId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnamnesisId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("RoomId");

                    b.ToTable("appointments");
                });

            modelBuilder.Entity("HealthInstitution.Model.AppointmentRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActivityType")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("PatientId");

                    b.ToTable("appointmentRequests");
                });

            modelBuilder.Entity("HealthInstitution.Model.Entry<HealthInstitution.Model.Equipment>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EquipmentTransferId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("TEXT");

                    b.Property<float>("Quantity")
                        .HasColumnType("REAL");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentTransferId");

                    b.HasIndex("ItemId");

                    b.HasIndex("RoomId");

                    b.ToTable("Entry<Equipment>");
                });

            modelBuilder.Entity("HealthInstitution.Model.Equipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("EquipmentType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("HealthInstitution.Model.EquipmentTransfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfTransfer")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DestinationRoomId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("OriginRoomId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DestinationRoomId");

                    b.HasIndex("OriginRoomId");

                    b.ToTable("EquipmentTransfers");
                });

            modelBuilder.Entity("HealthInstitution.Model.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("MedicineId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("HealthInstitution.Model.MedicalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("medicalRecords");
                });

            modelBuilder.Entity("HealthInstitution.Model.Medicine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Medicine");
                });

            modelBuilder.Entity("HealthInstitution.Model.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HealthInstitution.Model.RoomRenovation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RenovatedRoomId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RenovatedRoomId");

                    b.ToTable("RoomRenovations");
                });

            modelBuilder.Entity("HealthInstitution.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("HealthInstitution.Model.Administrator", b =>
                {
                    b.HasBaseType("HealthInstitution.Model.User");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("HealthInstitution.Model.Doctor", b =>
                {
                    b.HasBaseType("HealthInstitution.Model.User");

                    b.Property<int>("Specialization")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("HealthInstitution.Model.Manager", b =>
                {
                    b.HasBaseType("HealthInstitution.Model.User");

                    b.HasDiscriminator().HasValue("Manager");
                });

            modelBuilder.Entity("HealthInstitution.Model.Patient", b =>
                {
                    b.HasBaseType("HealthInstitution.Model.User");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("HealthInstitution.Model.Secretary", b =>
                {
                    b.HasBaseType("HealthInstitution.Model.User");

                    b.HasDiscriminator().HasValue("Secretary");
                });

            modelBuilder.Entity("HealthInstitution.Model.Activity", b =>
                {
                    b.HasOne("HealthInstitution.Model.Patient", null)
                        .WithMany("Activities")
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("HealthInstitution.Model.Appointment", b =>
                {
                    b.HasOne("HealthInstitution.Model.Anamnesis", "Anamnesis")
                        .WithMany()
                        .HasForeignKey("AnamnesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthInstitution.Model.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthInstitution.Model.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthInstitution.Model.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anamnesis");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HealthInstitution.Model.AppointmentRequest", b =>
                {
                    b.HasOne("HealthInstitution.Model.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthInstitution.Model.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HealthInstitution.Model.Entry<HealthInstitution.Model.Equipment>", b =>
                {
                    b.HasOne("HealthInstitution.Model.EquipmentTransfer", null)
                        .WithMany("TransferredEquipment")
                        .HasForeignKey("EquipmentTransferId");

                    b.HasOne("HealthInstitution.Model.Equipment", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthInstitution.Model.Room", null)
                        .WithMany("Inventory")
                        .HasForeignKey("RoomId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("HealthInstitution.Model.EquipmentTransfer", b =>
                {
                    b.HasOne("HealthInstitution.Model.Room", "DestinationRoom")
                        .WithMany()
                        .HasForeignKey("DestinationRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthInstitution.Model.Room", "OriginRoom")
                        .WithMany()
                        .HasForeignKey("OriginRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DestinationRoom");

                    b.Navigation("OriginRoom");
                });

            modelBuilder.Entity("HealthInstitution.Model.Ingredient", b =>
                {
                    b.HasOne("HealthInstitution.Model.Medicine", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("MedicineId");
                });

            modelBuilder.Entity("HealthInstitution.Model.MedicalRecord", b =>
                {
                    b.HasOne("HealthInstitution.Model.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HealthInstitution.Model.RoomRenovation", b =>
                {
                    b.HasOne("HealthInstitution.Model.Room", "RenovatedRoom")
                        .WithMany()
                        .HasForeignKey("RenovatedRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RenovatedRoom");
                });

            modelBuilder.Entity("HealthInstitution.Model.EquipmentTransfer", b =>
                {
                    b.Navigation("TransferredEquipment");
                });

            modelBuilder.Entity("HealthInstitution.Model.Medicine", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("HealthInstitution.Model.Room", b =>
                {
                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("HealthInstitution.Model.Patient", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
