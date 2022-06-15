using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.MedicalRecordManagement.Commands.PatientCMD;
using HealthInstitution.Core.Features.SurveyManagement.Commands.PatientCMD;
using HealthInstitution.Core.Features.SurveyManagement.Repository;
using HealthInstitution.Core.Features.SurveyManagement.Services;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.SurveyManagement
{
    public class DoctorSurveyViewModel : ViewModelBase
    {
        #region services
        private readonly IDoctorSurveyRepository _doctorSurveyRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        public IAppointmentRepository AppointmentRepository => _appointmentRepository;
        public IDoctorSurveyRepository DoctorSurveyRepository => _doctorSurveyRepository;
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

        public DoctorSurveyViewModel(IDoctorSurveyRepository doctorSurveyRepository, IAppointmentRepository appointmentRepository)
        {
            _doctorSurveyRepository = doctorSurveyRepository;
            _appointmentRepository = appointmentRepository;
            _selectedAppointment = GlobalStore.ReadObject<Appointment>("SelectedAppointment");
            ServiceQualityRating = 1;
            RecommendationRating = 1;

            FinishDoctorSurveyCommand = new FinishDoctorSurveyCommand(this);
            BackCommand = new PatientMedicalRecordCommand();
        }
    }
}
