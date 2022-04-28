using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class AppointmentCreationViewModel : ViewModelBase
    {
        public readonly IDoctorService _doctorService;
        public readonly IAppointmentService _appointmentService;
        public readonly IRoomService _roomService;
        public readonly IActivityService _activityService;
        public readonly IPatientService _patientService;

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string _hours;
        public string? Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                OnPropertyChanged(nameof(Hours));
            }
        } 
        
        private string _minutes;
        public string? Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                OnPropertyChanged(nameof(Minutes));
            }
        }

        private Doctor _selectedDoctor;
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }

        private List<Doctor> _doctors;
        public List<Doctor> Doctors 
        { 
            get => _doctors;
            set {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }
        public ICommand? MakeAppointmentCommand { get; }

        public AppointmentCreationViewModel(IDoctorService doctorService, IAppointmentService appointmentService, IRoomService roomService, IActivityService activityService, IPatientService patientService)
        {
            _activityService = activityService;
            _doctorService = doctorService;
            _appointmentService = appointmentService;
            _roomService = roomService;
            _patientService = patientService;
            Date = DateTime.Now;
            Doctors = doctorService.ReadAll().ToList();
            MakeAppointmentCommand = new MakeAppointmentCommand(this);
        }
    }
}
