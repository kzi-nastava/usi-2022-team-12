using System.Windows.Input;
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
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
        }

        private int _recommendationRating;
        public int RecommendationRating
        {
            get => _recommendationRating;
            set
            {
                _recommendationRating = value;
                OnPropertyChanged(nameof(RecommendationRating));
            }
        }

        private int _serviceQualityRating;
        public int ServiceQualityRating
        {
            get => _serviceQualityRating;
            set
            {
                _serviceQualityRating = value;
                OnPropertyChanged(nameof(ServiceQualityRating));
            }
        }

        private string _comment;
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
        public ICommand FinishDoctorSurveyCommand { get; }
        public ICommand BackCommand { get; }
        #region commands

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
