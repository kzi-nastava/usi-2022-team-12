using System.Collections.Generic;
using HealthInstitution.Model.appointment;
using HealthInstitution.Model.doctor;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Commands.doctor
{
    public class UndoMRChangesCommand : CommandBase
    {
        private ExaminationViewModel _viewModel;
        public override void Execute(object? parameter)
        {
            _viewModel.UpdatedMedicalRecord = new MedicalRecord(_viewModel.MedicalRecord);
            _viewModel.IllnessHistoryData = _viewModel.UpdatedMedicalRecord.IllnessHistory;
            _viewModel.Allergens = _viewModel.UpdatedMedicalRecord.Allergens;
            _viewModel.Anamnesis = "";
            _viewModel.NewIllnessName = "";
            _viewModel.NewAllergenName = "";
            _viewModel.NewIllnesses.Clear();
            _viewModel.NewAllergens.Clear();
            GlobalStore.AddObject("NewReferrals", new List<Referral>());
            GlobalStore.AddObject("Prescription", new List<PrescribedMedicine>());
        }

        public UndoMRChangesCommand(ExaminationViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
