﻿using System.Windows;
using HealthInstitution.Model.survey;
using HealthInstitution.Model.user;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel.patient;

namespace HealthInstitution.Commands.patient
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
            if (!_viewModel.HealthInstitutionSurveyService.HasPatientAlreadySubmitedSurvey(patient))
            {
                HealthInstitutionSurvey his = new HealthInstitutionSurvey { Patient = patient, ServiceQuality = _viewModel.ServiceQualityRating, Cleanliness = _viewModel.CleanlinessRating, ServiceSatisfaction = _viewModel.ServiceSatisfactionRating, Recommendation = _viewModel.RecommendationRating, Comment = _viewModel.Comment };
                _viewModel.HealthInstitutionSurveyService.Create(his);
                MessageBox.Show("Survey submited successfully.\nThank you for participating!");
            }
            else {
                MessageBox.Show("Survey already submitted!");
            }
        }
    }
}
