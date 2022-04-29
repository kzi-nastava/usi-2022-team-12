using HealthInstitution.Model;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
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
        }

        public UndoMRChangesCommand(ExaminationViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
