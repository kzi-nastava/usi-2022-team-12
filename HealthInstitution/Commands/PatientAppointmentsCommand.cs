using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
{
    public class PatientAppointmentsCommand : CommandBase
    {
        public PatientAppointmentsCommand() { 
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("PatientAppointments");
        }
    }
}
