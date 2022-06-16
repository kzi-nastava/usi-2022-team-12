using System.Collections.Generic;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.OperationsAndExaminations;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD
{
    public class BackToExaminationCommandFromReferral : CommandBase
    {
        DoctorReferralCreationViewModel _viewModel;
        public BackToExaminationCommandFromReferral(DoctorReferralCreationViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            List<Referral> newReferrals = new List<Referral>();
            foreach (Referral referral in _viewModel.Referrals)
            {
                newReferrals.Add(referral);
            }
            GlobalStore.AddObject("NewReferrals", newReferrals);
            EventBus.FireEvent("ReturnToExamination");
        }
    }
}
