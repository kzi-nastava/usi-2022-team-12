using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HealthInstitution
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Doctor> doctors = db.Doctors.ToList();

                Doctor doc = new Doctor();

                foreach(Doctor doctor in doctors)
                {
                    MessageBox.Show(doctor.FirstName);
                }
            }
        }
    }
}
