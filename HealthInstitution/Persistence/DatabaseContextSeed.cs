using HealthInstitution.Model;
using System;

namespace HealthInstitution.Persistence
{
    class DatabaseContextSeed
    {
        public static void Seed(DatabaseContext context)
        {

            //// Patients
            var p1 = new Patient { FirstName = "Petar", LastName = "Peric", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-20), EmailAddress = "petarperic@example.com", Role = Role.Patient, IsBlocked = true, NotificationPreference = 20 };
            var p2 = new Patient { FirstName = "Marko", LastName = "Markovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-35), EmailAddress = "markomarkovic@example.com", Role = Role.Patient, IsBlocked = false, NotificationPreference = 20 };
            var p3 = new Patient { FirstName = "Zeljko", LastName = "Nikolic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-50), EmailAddress = "example@example.com", Role = Role.Patient, IsBlocked = false, NotificationPreference = 20 };
            var p4 = new Patient { FirstName = "Milica", LastName = "Milic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-40), EmailAddress = "zeljkonikolic@example.com", Role = Role.Patient, IsBlocked = false, NotificationPreference = 20 };
            var p5 = new Patient { FirstName = "Zoran", LastName = "Gostojic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-38), EmailAddress = "zorangostojic@example.com", Role = Role.Patient, IsBlocked = false, NotificationPreference = 20 };
            var p6 = new Patient { FirstName = "Brusli", LastName = "Iljazovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-38), EmailAddress = "brusliiljazovic@example.com", Role = Role.Patient, IsBlocked = false, NotificationPreference = 20 };

            context.Patients.Add(p1);
            context.Patients.Add(p2);
            context.Patients.Add(p3);
            context.Patients.Add(p4);
            context.Patients.Add(p5);

            //Medical records
            var mr1 = new MedicalRecord(180.5, 70.4, p2);
            var alr1 = new Allergen("these nuts");
            var il1 = new Illness("paranoid");
            mr1.AddAllergen(alr1);
            mr1.AddIllness(il1);

            var mr2 = new MedicalRecord(179, 65.6, p6);

            context.MedicalRecords.Add(mr1);
            context.MedicalRecords.Add(mr2);

            // Secretaries
            var c1 = new Secretary { FirstName = "Nikola", LastName = "Petrovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-55), EmailAddress = "nikolapetrovic@example.com", Role = Role.Secretary };
            var c2 = new Secretary { FirstName = "Tamara", LastName = "Vujanovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-40), EmailAddress = "tamaravujanovic@example.com", Role = Role.Secretary };
            var c3 = new Secretary { FirstName = "Petar", LastName = "Blagojevic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-35), EmailAddress = "petarblagojevic@example.com", Role = Role.Secretary };

            context.Secretaries.Add(c1);
            context.Secretaries.Add(c2);
            context.Secretaries.Add(c3);


            // Doctors
            var d1 = new Doctor { FirstName = "Igor", LastName = "Mirkovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-48), EmailAddress = "igormirkovic@example.com", Role = Role.Doctor, Specialization = DoctorSpecialization.Pediatrician };
            var d2 = new Doctor { FirstName = "Veljko", LastName = "Vukovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-35), EmailAddress = "veljkovukovic@example.com", Role = Role.Doctor, Specialization = DoctorSpecialization.Pediatrician };
            var d3 = new Doctor { FirstName = "Gordana", LastName = "Milicic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-55), EmailAddress = "gordanamilicic@example.com", Role = Role.Doctor, Specialization = DoctorSpecialization.Cardiologist };

            context.Doctors.Add(d1);
            context.Doctors.Add(d2);
            context.Doctors.Add(d3);


            // Managers

            var m1 = new Manager { FirstName = "Velibor", LastName = "Stojkovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-60), EmailAddress = "veliborstojkovic@example.com", Role = Role.Manager };
            var m2 = new Manager { FirstName = "Radivoje", LastName = "Zivkovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-65), EmailAddress = "radivojezivkovic@example.com", Role = Role.Manager };

            context.Managers.Add(m1);
            context.Managers.Add(m2);

            // Administrator

            var a1 = new Administrator { FirstName = "Administrator", LastName = "Administrator", Password = "admin", DateOfBirth = DateTime.Now.AddYears(-60), EmailAddress = "admin", Role = Role.Administrator };

            context.Administrators.Add(a1);

            // Equipments

            var e1 = new Equipment { EquipmentType = EquipmentType.ExaminationEquipment, Name = "Chair" };
            var e2 = new Equipment { EquipmentType = EquipmentType.OperationEquipment, Name = "Operation table" };
            var e3 = new Equipment { EquipmentType = EquipmentType.Furniture, Name = "Sofa" };
            var e4 = new Equipment { EquipmentType = EquipmentType.ExaminationEquipment, Name = "Knife" };
            var e5 = new Equipment { EquipmentType = EquipmentType.HallwayEquipment, Name = "Hallway chair" };
            var e6 = new Equipment { EquipmentType = EquipmentType.Furniture, Name = "TV" };
            var e7 = new Equipment { EquipmentType = EquipmentType.ExaminationEquipment, Name = "Bed" };
            var e8 = new Equipment { EquipmentType = EquipmentType.OperationEquipment, Name = "Oxygen Generator" };

            context.Equipments.Add(e1);
            context.Equipments.Add(e2);
            context.Equipments.Add(e3);
            context.Equipments.Add(e4);
            context.Equipments.Add(e5);
            context.Equipments.Add(e6);
            context.Equipments.Add(e7);
            context.Equipments.Add(e8);

            // Ingredient
            var i1 = new Ingredient { Name = "Calcium" };

            context.Ingredients.Add(i1);

            // Medicine 
            var me1 = new Medicine();
            me1.Name = "Probiotic";
            me1.Description = "For stomach.";
            me1.AddIngredient(i1);

            context.Medicines.Add(me1);

            // Room
            var r1 = new Room(RoomType.Storage, "S");
            var r2 = new Room(RoomType.ExaminationRoom, "E1");
            var r3 = new Room(RoomType.ExaminationRoom, "E2");
            var r4 = new Room(RoomType.ExaminationRoom, "E3");
            var r5 = new Room(RoomType.OperationRoom, "O1");
            var r6 = new Room(RoomType.OperationRoom, "O2");
            var r7 = new Room(RoomType.OperationRoom, "O3");
            var r8 = new Room(RoomType.RestingRoom, "R1");
            var r9 = new Room(RoomType.RestingRoom, "R2");
            var r10 = new Room(RoomType.RestingRoom, "R3");

            // Storage equipment

            Entry<Equipment> entry1 = new Entry<Equipment> { Item = e1, Quantity = 15 };
            Entry<Equipment> entry2 = new Entry<Equipment> { Item = e2, Quantity = 5 };
            Entry<Equipment> entry3 = new Entry<Equipment> { Item = e3, Quantity = 7 };
            Entry<Equipment> entry4 = new Entry<Equipment> { Item = e4, Quantity = 50 };
            Entry<Equipment> entry5 = new Entry<Equipment> { Item = e5, Quantity = 30 };
            Entry<Equipment> entry6 = new Entry<Equipment> { Item = e6, Quantity = 10 };
            Entry<Equipment> entry7 = new Entry<Equipment> { Item = e7, Quantity = 20 };
            Entry<Equipment> entry8 = new Entry<Equipment> { Item = e8, Quantity = 3 };


            r1.AddEquipment(entry1);
            r1.AddEquipment(entry2);
            r1.AddEquipment(entry3);
            r1.AddEquipment(entry4);
            r1.AddEquipment(entry5);
            r1.AddEquipment(entry6);
            r1.AddEquipment(entry7);
            r1.AddEquipment(entry8);

            // ExaminationRoom equipment

            Entry<Equipment> entryE1 = new Entry<Equipment> { Item = e1, Quantity = 3 };
            Entry<Equipment> entryE2 = new Entry<Equipment> { Item = e4, Quantity = 5 };
            Entry<Equipment> entryE3 = new Entry<Equipment> { Item = e7, Quantity = 2 };
            Entry<Equipment> entryE4 = new Entry<Equipment> { Item = e1, Quantity = 3 };
            Entry<Equipment> entryE5 = new Entry<Equipment> { Item = e4, Quantity = 5 };
            Entry<Equipment> entryE6 = new Entry<Equipment> { Item = e7, Quantity = 2 };
            Entry<Equipment> entryE7 = new Entry<Equipment> { Item = e1, Quantity = 3 };
            Entry<Equipment> entryE8 = new Entry<Equipment> { Item = e4, Quantity = 5 };
            Entry<Equipment> entryE9 = new Entry<Equipment> { Item = e7, Quantity = 2 };

            r2.AddEquipment(entryE1);
            r2.AddEquipment(entryE2);
            r2.AddEquipment(entryE3);

            r3.AddEquipment(entryE4);
            r3.AddEquipment(entryE5);
            r3.AddEquipment(entryE6);

            r4.AddEquipment(entryE7);
            r4.AddEquipment(entryE8);
            r4.AddEquipment(entryE9);

            // OperationRoom equipment

            Entry<Equipment> entryO1 = new Entry<Equipment> { Item = e2, Quantity = 2 };
            Entry<Equipment> entryO2 = new Entry<Equipment> { Item = e8, Quantity = 1 };
            Entry<Equipment> entryO3 = new Entry<Equipment> { Item = e2, Quantity = 1 };
            Entry<Equipment> entryO4 = new Entry<Equipment> { Item = e2, Quantity = 1 };
            Entry<Equipment> entryO5 = new Entry<Equipment> { Item = e8, Quantity = 1 };

            r5.AddEquipment(entryO1);
            r5.AddEquipment(entryO2);

            r6.AddEquipment(entryO3);

            r7.AddEquipment(entryO4);
            r7.AddEquipment(entryO5);

            // RestingRoom equipment

            Entry<Equipment> entryR1 = new Entry<Equipment> { Item = e3, Quantity = 4 };
            Entry<Equipment> entryR2 = new Entry<Equipment> { Item = e6, Quantity = 1 };
            Entry<Equipment> entryR3 = new Entry<Equipment> { Item = e3, Quantity = 4 };
            Entry<Equipment> entryR4 = new Entry<Equipment> { Item = e6, Quantity = 1 };
            Entry<Equipment> entryR5 = new Entry<Equipment> { Item = e3, Quantity = 4 };
            Entry<Equipment> entryR6 = new Entry<Equipment> { Item = e6, Quantity = 1 };

            r8.AddEquipment(entryR1);
            r8.AddEquipment(entryR2);

            r9.AddEquipment(entryR3);
            r9.AddEquipment(entryR4);

            r10.AddEquipment(entryR5);
            r10.AddEquipment(entryR6);


            context.Rooms.Add(r1);
            context.Rooms.Add(r2);
            context.Rooms.Add(r3);
            context.Rooms.Add(r4);
            context.Rooms.Add(r5);
            context.Rooms.Add(r6);
            context.Rooms.Add(r7);
            context.Rooms.Add(r8);
            context.Rooms.Add(r9);
            context.Rooms.Add(r10);


            // Appointments
            //var ap1 = new Appointment(d1, p1, DateTime.Now, DateTime.Now.AddMinutes(15), r2, null, false);

            //var ap2 = new Appointment(d2, p2, DateTime.Now, DateTime.Now.AddMinutes(15), r2, null, false);

            //var ap3 = new Appointment(d1, p3, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddMinutes(15), r2, null, false);

            //context.Appointments.Add(ap1);
            //context.Appointments.Add(ap2);
            //context.Appointments.Add(ap3);

            // Appointment update requests
            //var aru1 = new AppointmentUpdateRequest()
            //{
            //    Patient = p1,
            //    Appointment = ap1,
            //    ActivityType = ActivityType.Update,
            //    Status = Status.Pending,
            //    StartDate = DateTime.Now.AddDays(2),
            //    EndDate = DateTime.Now.AddDays(2).AddMinutes(15),
            //    Doctor = d2,
            //    Room = r2
            //};

            //var aru2 = new AppointmentUpdateRequest()
            //{
            //    Patient = p2,
            //    Appointment = ap2,
            //    ActivityType = ActivityType.Update,
            //    Status = Status.Pending,
            //    StartDate = DateTime.Now.AddDays(2),
            //    EndDate = DateTime.Now.AddDays(2).AddMinutes(15),
            //    Doctor = d2,
            //    Room = r2
            //};

            //context.AppointmentUpdateRequests.Add(aru1);
            //context.AppointmentUpdateRequests.Add(aru2);

            // Appointment delete requests

            //var adr1 = new AppointmentDeleteRequest() { Patient = p1, Appointment = ap1, ActivityType = ActivityType.Delete, Status = Status.Pending };

            //context.AppointmentDeleteRequests.Add(adr1);

            context.SaveChanges();
        }
    }
}
