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
    public class BackToExaminationCommand : CommandBase
    {
        DoctorReferralCreationViewModel _viewModel;
        public BackToExaminationCommand(DoctorReferralCreationViewModel viewModel) {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            List<Referral> newReferrals = new List<Referral>();
            foreach(Referral referral in _viewModel.Referrals)
            {
                newReferrals.Add(referral);
            }
            GlobalStore.AddObject("NewReferrals", newReferrals);
            EventBus.FireEvent("ReturnToExamination");
        }
    }
}
