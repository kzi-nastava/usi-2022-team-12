using HealthInstitution.Model;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
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
