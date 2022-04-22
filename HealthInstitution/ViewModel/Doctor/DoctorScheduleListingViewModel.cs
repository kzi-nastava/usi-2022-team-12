using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.ViewModel
{
    public class DoctorScheduleListingViewModel : ViewModelBase
    {
        public class AppointmentViewModel : ViewModelBase
        {
            private readonly Appointment _appointment;

            public string PatientName
            {
                get
                {
                    return _appointment.Patient.FullName;
                }
            }
            public string Date
            {
                get
                {
                    return _appointment.StartDate.ToString("D");
                }
            }
            public string Time
            {
                get
                {
                    return _appointment.StartDate.ToString("t");
                }
            }
            public string Room
            {
                get
                {
                    return _appointment.Room.Name;
                }
            }
            public AppointmentViewModel(Appointment appointment)
            {
                _appointment = appointment;
            }
        }

        public readonly IAppointmentService _appointmentService;

        private readonly ObservableCollection<AppointmentViewModel> _appointments;

        public IEnumerable<AppointmentViewModel> Appointments => _appointments;

        public DoctorScheduleListingViewModel(IAppointmentService appointemntService)
        {
            _appointmentService = appointemntService;
            _appointments = new ObservableCollection<AppointmentViewModel>();
            UpdateData(appointemntService.ReadAll());

        }

        public void UpdateData(IEnumerable<Appointment> appointments)
        {
            _appointments.Clear();
            foreach (Appointment appointment in appointments)
            {
                _appointments.Add(new AppointmentViewModel(appointment));
            }
        }
    }
}
