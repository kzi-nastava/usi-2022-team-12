using HealthInstitution.Model;
using HealthInstitution.Services.Implementation;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.ViewModel
{
    public class DoctorScheduleViewModel : ViewModelBase
    {
        public class AppointmentViewModel : ViewModelBase
        {
            private readonly Appointment _appointment;
            public string PatientName => _appointment.Patient.FullName;              
            public string Date => _appointment.StartDate.ToString("D");                
            public string Time => _appointment.StartDate.ToString("t");                
            public string Room => _appointment.Room.Name;
            public AppointmentViewModel(Appointment appointment)
            {
                _appointment = appointment;
            }
        }
        private DateTime _userDate;
        public DateTime UserDate
        {
            get => _userDate;
            set
            {
                _userDate = value;
                UpdateData();
                OnPropertyChanged(nameof(UserDate));
            }
        }

        private bool _next3Days;

        public bool Next3Days
        {
            get => _next3Days;
            set
            {
                _next3Days = value;
                UpdateData();
                OnPropertyChanged(nameof(Next3Days));
            }
        }
        private DateTime EndDate
        {
            get
            {
                if (Next3Days)
                {
                    return UserDate.AddDays(3);
                }
                else
                {
                    return UserDate;
                }
            }
        }
        public readonly AppointmentService _appointmentService;

        private readonly ObservableCollection<AppointmentViewModel> _appointments;

        public IEnumerable<AppointmentViewModel> Appointments => _appointments;

        public DoctorScheduleViewModel(AppointmentService appointemntService)
        {
            _userDate = DateTime.Now;
            _appointmentService = appointemntService;
            _appointments = new ObservableCollection<AppointmentViewModel>();
            UpdateData();

        }

        public void UpdateData()
        {
            DateTime startDate = UserDate;
            DateTime endDate = EndDate;
            Doctor doctor = GlobalStore.ReadObject<Doctor>("LoggedUser");
            IEnumerable<Appointment> appointments = _appointmentService.GetAppointmentsForDateRangeAndDoctor(startDate, endDate, doctor);
            _appointments.Clear();
            foreach (Appointment appointment in appointments)
            {
                _appointments.Add(new AppointmentViewModel(appointment));
            }
            OnPropertyChanged(nameof(Appointments));
        }
    }
}
