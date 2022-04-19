using HealthInstitution.Model;
using System;

namespace HealthInstitution.Persistence
{
    class DatabaseContextSeed
    {
        public static void Seed(DatabaseContext context)
        {
            // Patients
            var p1 = new Patient { FirstName = "Petar", LastName = "Peric", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-20), EmailAddress = "petarperic" };
            var p2 = new Patient { FirstName = "Marko", LastName = "Markovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-35), EmailAddress = "markomarkovic" };
            var p3 = new Patient { FirstName = "Zeljko", LastName = "Nikolic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-50), EmailAddress = "example@gmail.com" };
            var p4 = new Patient { FirstName = "Milica", LastName = "Milic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-40), EmailAddress = "zeljkonikolic" };
            var p5 = new Patient { FirstName = "Zoran", LastName = "Gostojic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-38), EmailAddress = "zorangostojic" };

            context.Patients.Add(p1);
            context.Patients.Add(p2);
            context.Patients.Add(p3);
            context.Patients.Add(p4);
            context.Patients.Add(p5);


            // Secretaries
            var c1 = new Secretary { FirstName = "Nikola", LastName = "Petrovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-55), EmailAddress = "nikolapetrovic" };
            var c2 = new Secretary { FirstName = "Tamara", LastName = "Vujanovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-40), EmailAddress = "tamaravujanovic" };
            var c3 = new Secretary { FirstName = "Petar", LastName = "Blagojevic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-35), EmailAddress = "petarblagojevic" };

            context.Secretaries.Add(c1);
            context.Secretaries.Add(c2);
            context.Secretaries.Add(c3);


            // Doctors
            var d1 = new Doctor { FirstName = "Igor", LastName = "Mirkovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-48), EmailAddress = "igormirkovic" };
            var d2 = new Doctor { FirstName = "Veljko", LastName = "Vukovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-35), EmailAddress = "veljkovukovic" };
            var d3 = new Doctor { FirstName = "Gordana", LastName = "Milicic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-55), EmailAddress = "gordanamilicic" };

            context.Doctors.Add(d1);
            context.Doctors.Add(d2);
            context.Doctors.Add(d3);


            // Managers

            var m1 = new Manager { FirstName = "Velibor", LastName = "Stojkovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-60), EmailAddress = "veliborstojkovic" };
            var m2 = new Manager { FirstName = "Radivoje", LastName = "Zivkovic", Password = "test123", DateOfBirth = DateTime.Now.AddYears(-65), EmailAddress = "radivojezivkovic" };

            context.Managers.Add(m1);
            context.Managers.Add(m2);

            // Administrator

            var a1 = new Administrator { FirstName = "Administrator", LastName = "Administrator", Password = "admin", DateOfBirth = DateTime.Now.AddYears(-60), EmailAddress = "admin" };

            context.Administrators.Add(a1);

            // Equipments

            var e1 = new Equipment { EquipmentType = EquipmentType.HallwayEquipment, Name = "Chair" };
            var e2 = new Equipment { EquipmentType = EquipmentType.OperationEquipment, Name = "Operation table" };
            var e3 = new Equipment { EquipmentType = EquipmentType.Furniture, Name = "Sofa" };
            var e4 = new Equipment { EquipmentType = EquipmentType.ExaminationEquipment, Name = "Knife" };
            var e5 = new Equipment { EquipmentType = EquipmentType.HallwayEquipment, Name = "Hallway chair" };

            context.Equipments.Add(e1);
            context.Equipments.Add(e2);
            context.Equipments.Add(e3);
            context.Equipments.Add(e4);
            context.Equipments.Add(e5);

            // Ingredient
            var i1 = new Ingredient { Name = "Kalcijum" };

            context.Ingredients.Add(i1);

            // Medicine 
            var me1 = new Medicine("Opis leka");
            me1.AddIngredient(i1);

            context.Medicines.Add(me1);

            context.SaveChanges();
        }
    }
}
