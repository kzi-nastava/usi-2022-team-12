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
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }
        public ICommand? UpdateAppointmentCommand { get; }

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

        public AppointmentUpdateViewModel(IDoctorService doctorService, IAppointmentService appointmentService, IRoomService roomService, IAppointmentUpdateRequestService appointmentUpdateRequestService, IActivityService activityService, IPatientService patientService)
        {
            ChosenAppointment = GlobalStore.ReadObject<Appointment>("ChosenAppointment");
            this.appointmentService = appointmentService;
            this.activityService = activityService;
            this.patientService = patientService;
            Doctors = doctorService.ReadAll().ToList();

            Date = ChosenAppointment.StartDate.Date;
            SelectedDoctor = ChosenAppointment.Doctor;
            Hours = ChosenAppointment.StartDate.Hour.ToString();
            Minutes = ChosenAppointment.StartDate.Minute.ToString();

            UpdateAppointmentCommand = new UpdateAppointmentCommand(this);
        }
    }
}
