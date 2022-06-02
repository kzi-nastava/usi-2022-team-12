using HealthInstitution.Model;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands
{
    public class FinishDoctorSurveyCommand : CommandBase
    {
        private readonly DoctorSurveyViewModel? _viewModel;
        public FinishDoctorSurveyCommand(DoctorSurveyViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            DoctorSurvey ds = new DoctorSurvey { Doctor = _viewModel.SelectedAppointment.Doctor, ServiceQuality = _viewModel.ServiceQualityRating, Recommendation = _viewModel.RecommendationRating, Comment = _viewModel.Comment};
            _viewModel.DoctorSurveyService.Create(ds);
            _viewModel.SelectedAppointment.IsRated = true;
            _viewModel.AppointmentService.Update(_viewModel.SelectedAppointment);
            MessageBox.Show("Doctor rated successfully.\nThank you for participating!");
            EventBus.FireEvent("PatientMedicalRecord");
        }
    }
}
