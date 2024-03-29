﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.AppointmentScheduling
{
    public class AppointmentUpdateViewModel : ViewModelBase
    {
        #region services
        private readonly ISchedulingService _schedulingService;
        private readonly IActivityService _activityService;
        private readonly IPatientService _patientService;

        public ISchedulingService SchedulingService => _schedulingService;
        public IActivityService ActivityService => _activityService;
        public IPatientService PatientService => _patientService;
        #endregion endregion

        #region attributes
        private DateTime _startDate;
        private DateTime _startTime;
        private Doctor _selectedDoctor;
        private List<Doctor> _doctors;
        private Appointment _selectedAppointment;
        #endregion

        #region properties
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }
        public List<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }
        #endregion

        #region commands
        public ICommand? UpdateAppointmentCommand { get; }
        public ICommand? BackCommand { get; }
        #endregion

        public AppointmentUpdateViewModel(IDoctorRepository doctorService, IPatientService patientService, ISchedulingService schedulingService, IActivityService activityService)
        {
            SelectedAppointment = GlobalStore.ReadObject<Appointment>("SelectedAppointment");
            _activityService = activityService;
            _schedulingService = schedulingService;
            _patientService = patientService;

            Doctors = doctorService.ReadAll().OrderBy(doc => doc.Specialization).ToList();

            StartDate = SelectedAppointment.StartDate;
            StartTime = SelectedAppointment.StartDate;
            SelectedDoctor = SelectedAppointment.Doctor;

            UpdateAppointmentCommand = new UpdateAppointmentCommand(this);
            BackCommand = new PatientAppointmentsCommand();
        }
    }
}
