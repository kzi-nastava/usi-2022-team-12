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
        #region services
        public readonly IAppointmentService appointmentService;
        public readonly IActivityService activityService;
        public readonly IDoctorService doctorService;
        public readonly IPatientService patientService;
        #endregion

        #region attributes
        private DateTime _startDateTime;
        public DateTime StartDateTime
        {
            get => _startDateTime;
            set
            {
                _startDateTime = value;
                OnPropertyChanged(nameof(StartDateTime));
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
        #endregion

        #region commands
        public ICommand? MakeAppointmentCommand { get; }
        public ICommand? PatientAppointmentsCommand { get; }
        #endregion

        public AppointmentCreationViewModel(IDoctorService doctorService, IPatientService patientService, IAppointmentService appointmentService, IActivityService activityService)
        {
            this.activityService = activityService;
            this.appointmentService = appointmentService;
            this.doctorService = doctorService;
            this.patientService = patientService;
            StartDateTime = DateTime.Now;
            Doctors = this.doctorService.ReadAll().ToList();
            MakeAppointmentCommand = new MakeAppointmentCommand(this);
            PatientAppointmentsCommand = new PatientAppointmentsCommand();
        }
    }
}
