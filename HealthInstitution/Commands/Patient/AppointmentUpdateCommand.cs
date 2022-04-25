using HealthInstitution.Model;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands
{
    public class AppointmentUpdateCommand : CommandBase
    {
        public AppointmentUpdateCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            if (DateTime.Now.AddDays(1) > GlobalStore.ReadObject<Appointment>("ChosenAppointment").StartDate)
            {
                MessageBox.Show("You can't update appointment in last 24h!");
            }
            else
            {
                EventBus.FireEvent("AppointmentUpdate");
            }
        }
    }
}
