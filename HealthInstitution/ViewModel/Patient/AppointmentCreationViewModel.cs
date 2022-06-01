﻿using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
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
        private readonly IAppointmentService _appointmentService;
        private readonly IActivityService _activityService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public IAppointmentService AppointmentService => _appointmentService;
        public IActivityService ActivityService => _activityService;
        public IDoctorService DoctorService => _doctorService;
        public IPatientService PatientService => _patientService;
        #endregion

        #region attributes
        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
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
        public ICommand? BackCommand { get; }
        #endregion

        public AppointmentCreationViewModel(IDoctorService doctorService, IPatientService patientService, IAppointmentService appointmentService, IActivityService activityService)
        {
            _activityService = activityService;
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;

            DateTime currentDateTime = DateTime.Now;
            StartDate = currentDateTime.Date;
            StartTime = currentDateTime.Date.AddHours(currentDateTime.Hour).AddMinutes(currentDateTime.Minute);
            Doctors = DoctorService.ReadAll().OrderBy(doc => doc.Specialization).ToList();

            MakeAppointmentCommand = new MakeAppointmentCommand(this);
            BackCommand = new PatientAppointmentsCommand();
        }

        public AppointmentCreationViewModel(Doctor selectedDoctor)
        {
            _activityService = ServiceLocator.Get<IActivityService>();
            _appointmentService = ServiceLocator.Get<IAppointmentService>();
            _doctorService = ServiceLocator.Get<IDoctorService>();
            _patientService = ServiceLocator.Get<IPatientService>();

            DateTime currentDateTime = DateTime.Now;
            StartDate = currentDateTime.Date;
            StartTime = currentDateTime.Date.AddHours(currentDateTime.Hour).AddMinutes(currentDateTime.Minute);
            Doctors = new List<Doctor>() {selectedDoctor};
            SelectedDoctor = selectedDoctor;

            MakeAppointmentCommand = new MakeAppointmentCommand(this);
            BackCommand = new DoctorSearchCommand();
        }
    }
}
