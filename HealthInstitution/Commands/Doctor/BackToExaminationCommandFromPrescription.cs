using System.Collections.Generic;
using HealthInstitution.Model.doctor;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Commands.doctor
{
    public class BackToExaminationCommandFromPrescription : CommandBase
    {
        PrescriptionViewModel _viewModel;
        public BackToExaminationCommandFromPrescription(PrescriptionViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            List<PrescribedMedicine> newPrescriptions = new List<PrescribedMedicine>();
            foreach(PrescribedMedicine prescribedMedicine in _viewModel.PrescribedMedicines)
            {
                newPrescriptions.Add(prescribedMedicine);
            }
            GlobalStore.AddObject("Prescription", newPrescriptions);
            EventBus.FireEvent("ReturnToExamination");
        }
    }
}
