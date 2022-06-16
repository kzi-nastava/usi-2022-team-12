using System.Collections.Generic;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.OperationsAndExaminations;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD
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
            foreach (PrescribedMedicine prescribedMedicine in _viewModel.PrescribedMedicines)
            {
                newPrescriptions.Add(prescribedMedicine);
            }
            GlobalStore.AddObject("Prescription", newPrescriptions);
            EventBus.FireEvent("ReturnToExamination");
        }
    }
}
