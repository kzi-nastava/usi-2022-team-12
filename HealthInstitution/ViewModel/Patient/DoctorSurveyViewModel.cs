﻿using System.Windows.Input;
using HealthInstitution.Commands.patient;
using HealthInstitution.Commands.patient.Navigation;
using HealthInstitution.Model.appointment;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;

namespace HealthInstitution.ViewModel.patient
{
    public class DoctorSurveyViewModel : ViewModelBase
    {
        #region services
        private readonly IDoctorSurveyService _doctorSurveyService;
        private readonly IAppointmentService _appointmentService;
        public IDoctorSurveyService DoctorSurveyService => _doctorSurveyService;
        public IAppointmentService AppointmentService => _appointmentService;
        #endregion

        #region attributes
        private Appointment _selectedAppointment;
        private int _recommendationRating;
        private int _serviceQualityRating;
        private string _comment;
        #endregion

        #region properties
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
        }
        public int RecommendationRating
        {
            get => _recommendationRating;
            set
            {
                _recommendationRating = value;
                OnPropertyChanged(nameof(RecommendationRating));
            }
        }
        public int ServiceQualityRating
        {
            get => _serviceQualityRating;
            set
            {
                _serviceQualityRating = value;
                OnPropertyChanged(nameof(ServiceQualityRating));
            }
        }
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        #endregion

        #region commands
        public ICommand FinishDoctorSurveyCommand { get; }
        public ICommand BackCommand { get; }
        #endregion

        public DoctorSurveyViewModel(IDoctorSurveyService doctorSurveyService, IAppointmentService appointmentService)
        {
            _doctorSurveyService = doctorSurveyService;
            _appointmentService = appointmentService;
            _selectedAppointment = GlobalStore.ReadObject<Appointment>("SelectedAppointment");
            ServiceQualityRating = 1;
            RecommendationRating = 1;

            FinishDoctorSurveyCommand = new FinishDoctorSurveyCommand(this);
            BackCommand = new PatientMedicalRecordCommand();
        }
    }
}
