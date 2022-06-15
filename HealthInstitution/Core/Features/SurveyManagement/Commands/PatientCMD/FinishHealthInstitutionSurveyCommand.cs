using System.Windows;
using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.SurveyManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.SurveyManagement.Commands.PatientCMD
{
    public class FinishHealthInstitutionSurveyCommand : CommandBase
    {
        private readonly HealthInstitutionSurveyViewModel? _viewModel;
        public FinishHealthInstitutionSurveyCommand(HealthInstitutionSurveyViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            if (!_viewModel.HealthInstitutionSurveyRepository.HasPatientAlreadySubmitedSurvey(patient))
            {
                HealthInstitutionSurvey his = new HealthInstitutionSurvey { Patient = patient, ServiceQuality = _viewModel.ServiceQualityRating, Cleanliness = _viewModel.CleanlinessRating, ServiceSatisfaction = _viewModel.ServiceSatisfactionRating, Recommendation = _viewModel.RecommendationRating, Comment = _viewModel.Comment };
                _viewModel.HealthInstitutionSurveyRepository.Create(his);
                MessageBox.Show("Survey submited successfully.\nThank you for participating!");
            }
            else {
                MessageBox.Show("Survey already submitted!");
            }
        }
    }
}
