using System;
using System.Windows.Controls;

namespace HealthInstitution.View.doctor
{
    /// <summary>
    /// Interaction logic for DoctorAppointmentCreationView.xaml
    /// </summary>
    public partial class DoctorAppointmentCreationView : UserControl
    {
        public DoctorAppointmentCreationView()
        {
            InitializeComponent();
            StartDatePicker.DisplayDateStart = DateTime.Today.AddDays(1);
            
        }
    }
}
