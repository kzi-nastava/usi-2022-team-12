using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class AppointmentCreationViewModel : ViewModelBase
    {
        public readonly IDoctorService _doctorService;
        public readonly IAppointmentService _appointmentService;

        private DateTime? _date;
        public DateTime? Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string _time;
        public string? Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
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
        public ICommand? AppointmentCreationCommand { get; }

        public AppointmentCreationViewModel(IDoctorService doctorService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _appointmentService = appointmentService;
            Doctors = doctorService.ReadAll().ToList();
        }
    }
}
