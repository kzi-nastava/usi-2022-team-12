using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands.doctor.Navigation
{
    public class NavigateOffDaysRequestCreationCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("CreateOffDayRequest");
        }
    }
}
