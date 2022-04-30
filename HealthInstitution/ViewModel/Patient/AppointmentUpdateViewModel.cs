using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class AppointmentUpdateViewModel : ViewModelBase
    {
        public readonly IAppointmentService appointmentService;
        public readonly IActivityService activityService;
        public readonly IPatientService patientService;
        public readonly IDoctorService doctorService;

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
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }

        private Appointment _chosenAppointment;
        public Appointment ChosenAppointment
        {
            get => _chosenAppointment;
            set
            {
                _chosenAppointment = value;
                OnPropertyChanged(nameof(ChosenAppointment));
            }
        }

        public ICommand? UpdateAppointmentCommand { get; }
        public ICommand? PatientAppointmentsCommand { get; }

        public AppointmentUpdateViewModel(IDoctorService doctorService, IAppointmentService appointmentService, IActivityService activityService, IPatientService patientService)
        {
            ChosenAppointment = GlobalStore.ReadObject<Appointment>("ChosenAppointment");
            this.appointmentService = appointmentService;
            this.activityService = activityService;
            this.patientService = patientService;
            this.doctorService = doctorService;
            Doctors = doctorService.ReadAll().ToList();

            StartDateTime = ChosenAppointment.StartDate;
            SelectedDoctor = ChosenAppointment.Doctor;

            UpdateAppointmentCommand = new UpdateAppointmentCommand(this);
            PatientAppointmentsCommand = new PatientAppointmentsCommand();
        }
    }
}
