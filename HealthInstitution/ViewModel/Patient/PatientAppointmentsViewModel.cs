﻿using HealthInstitution.Commands;
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
    public class PatientAppointmentsViewModel : ViewModelBase
    {
        #region services
        public readonly IAppointmentService appointmentService;
        public readonly IAppointmentDeleteRequestService appointmentDeleteRequestService;
        public readonly IActivityService activityService;
        public readonly IPatientService patientService;
        #endregion

        #region attributes
        private List<Appointment> _futureAppointments;
        public List<Appointment> FutureAppointments
        {
            get => _futureAppointments;
            set
            {
                _futureAppointments = value;
                OnPropertyChanged(nameof(FutureAppointments));
            }
        }

        private List<Appointment> _pastAppointments;
        public List<Appointment> PastAppointments
        {
            get => _pastAppointments;
            set
            {
                _pastAppointments = value;
                OnPropertyChanged(nameof(PastAppointments));
            }
        }

        private Appointment _selectedAppointment;
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                GlobalStore.AddObject("ChosenAppointment", value);
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }
        #endregion

        #region commands
        public ICommand? AppointmentCreationCommand { get; }
        public ICommand? AppointmentUpdateCommand { get; }
        public ICommand? RemoveAppointmentCommand { get; }
        #endregion

        public PatientAppointmentsViewModel(IAppointmentService appointmentService, IAppointmentDeleteRequestService appointmentDeleteRequestService, IActivityService activityService, IPatientService patientService)
        {
            this.appointmentService = appointmentService;
            this.appointmentDeleteRequestService = appointmentDeleteRequestService;
            this.activityService = activityService;
            this.patientService = patientService;
            Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
            FutureAppointments = this.appointmentService.ReadFuturePatientAppointments(pt).OrderByDescending(apt => apt.StartDate).ToList();
            PastAppointments = this.appointmentService.ReadFinishedAppointmentsForPatient(pt).OrderByDescending(apt => apt.StartDate).ToList();
            AppointmentCreationCommand = new AppointmentCreationCommand();
            AppointmentUpdateCommand = new AppointmentUpdateCommand(this);
            RemoveAppointmentCommand = new RemoveAppointmentCommand(this);
        }
    }
}
