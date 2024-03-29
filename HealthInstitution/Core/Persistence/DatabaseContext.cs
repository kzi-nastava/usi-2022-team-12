﻿using System;
using System.IO;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Features.OffDaysManagement.Model;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthInstitution.Core.Persistence
{
    public class DatabaseContext : DbContext
    {

        public string DbPath { get; }

        // Users
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        // Medicine
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineReview> MedicineReviews { get; set; }

        // Room related things
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentTransfer> EquipmentTransfers { get; set; }
        public DbSet<RoomRenovation> RoomRenovations { get; set; }

        // Doctor related things
        public DbSet<DoctorSurvey> DoctorSurveys { get; set; }
        public DbSet<OffDaysRequest> OffDaysRequests { get; set; }
        // Patient related things
        public DbSet<Activity> Activities { get; set; }
        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }
        public DbSet<PrescribedMedicineNotification> PrescribedMedicinesNotifications { get; set; }
        public DbSet<AppointmentUpdateRequest> AppointmentUpdateRequests { get; set; }
        public DbSet<AppointmentDeleteRequest> AppointmentDeleteRequests { get; set; }

        // Appointment related things
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<Illness> Illness { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        // Secretary related things
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<EquipmentPurchaseRequest> EquipmentPurchaseRequests { get; set; }

        // Health Institution related things
        public DbSet<HealthInstitutionSurvey> HealthInstitutionSurveys { get; set; }

        public DatabaseContext()
        {
            var folder = Directory.GetCurrentDirectory();
            DbPath = Path.Join(folder, "Core/Persistence");
            DbPath = Path.Join(DbPath, "health.db");
        }

        public DatabaseContext(int dummyArg)
        {
            var folder = Directory.GetCurrentDirectory();
            DbPath = Path.Join(folder, "..\\..\\..\\Core/Persistence");
            DbPath = Path.Join(DbPath, "health.db");
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "health.db");
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = System.IO.Path.Join(path, "health.db");

            //DbPath = System.IO.Path.Join("Persistence\\Database\\", "health.db");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseLazyLoadingProxies().UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
