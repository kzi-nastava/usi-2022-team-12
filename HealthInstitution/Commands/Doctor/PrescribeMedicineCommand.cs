using System.ComponentModel;
using System.Windows;
using HealthInstitution.Model.appointment;
using HealthInstitution.Model.doctor;
using HealthInstitution.Model.medicine;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Commands.doctor
{
    public class PrescribeMedicineCommand : CommandBase
    {
        private PrescriptionViewModel _viewModel;

        public PrescribeMedicineCommand(PrescriptionViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedMedicine))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedMedicine is not null && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            if (IsAlergic())
            {
                MessageBox.Show("Patient is allergic to the prescribed medicine. Prescription canceled.");
                return;
            }
            if(_viewModel.UsageStart > _viewModel.UsageEnd)
            {
                MessageBox.Show("Usage start date cannot be after usage end date!");
                return;
            }
            PrescribedMedicine prescibedMedicine = new PrescribedMedicine{ Instruction = _viewModel.Instruction, Medicine = _viewModel.SelectedMedicine, UsageStart = _viewModel.UsageStart, UsageEnd = _viewModel.UsageEnd, UsageHourSpan = _viewModel.UsageHourSpan, MedicalRecord = _viewModel.MedicalRecord};
            _viewModel.addPrescribedMedicine(prescibedMedicine);
        }

        private bool IsAlergic()
        {
            Medicine medicine = _viewModel.SelectedMedicine;
            foreach(Allergen allergen in _viewModel.MedicalRecord.Allergens)
            {
                foreach(Ingredient ingredient in medicine.Ingredients)
                {
                    if (ingredient.Name.ToLower().Equals(allergen.Name.ToLower()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
