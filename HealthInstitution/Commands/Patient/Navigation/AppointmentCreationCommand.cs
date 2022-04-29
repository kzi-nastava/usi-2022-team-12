using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
{
    public class AppointmentCreationCommand : CommandBase
    {
        public AppointmentCreationCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("AppointmentCreation");
        }
    }
}
