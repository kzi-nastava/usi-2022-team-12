using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands.doctor.Navigation
{
    public class NavigateOffDaysCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("DoctorOffDays");
        }
    }
}
