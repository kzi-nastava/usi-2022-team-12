using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.AppointmentScheduling;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD
{
    public class UpdateAppointmentCommand : CommandBase
    {
        private readonly AppointmentUpdateViewModel? _viewModel;
        public UpdateAppointmentCommand(AppointmentUpdateViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.StartDate) || e.PropertyName == nameof(_viewModel.StartTime) || e.PropertyName == nameof(_viewModel.SelectedDoctor))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.StartDate.Date.AddMinutes(_viewModel.StartTime.TimeOfDay.TotalMinutes) <= DateTime.Now) && !(_viewModel.SelectedDoctor == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            DateTime startDateTime = _viewModel.StartDate.Date.AddMinutes(_viewModel.StartTime.TimeOfDay.TotalMinutes);
            DateTime endDateTime = startDateTime.AddMinutes(15);

            try
            {
                Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
                var updated = _viewModel.AppointmentService.UpdateAppointment(_viewModel.SelectedAppointment, pt, _viewModel.SelectedDoctor, startDateTime, endDateTime);

                if (updated)
                {
                    MessageBox.Show("Appointment updated successfully!");

                }
                else {
                    MessageBox.Show("Request for appointment update created successfully!\nPlease wait for secretary to review it.");
                }

                Activity act = new Activity(pt, DateTime.Now, ActivityType.Update);
                _viewModel.ActivityService.Create(act);

                var activityCount = _viewModel.ActivityService.ReadPatientUpdateOrRemoveActivity(pt, 30).ToList<Activity>().Count;
                if (activityCount >= 5)
                {
                    pt.IsBlocked = true;
                    _viewModel.PatientService.Update(pt);
                    MessageBox.Show("Your profile has been blocked!\n(Too many appointments removed or updated)");
                    EventBus.FireEvent("BackToLogin");
                }
                else
                {
                    EventBus.FireEvent("PatientAppointments");
                }
            }
            catch (DoctorBusyException)
            {
                MessageBox.Show("Selected doctor is busy at selected time!");
            }
            catch (RoomBusyException)
            {
                MessageBox.Show("All rooms are busy at selected time!");
            }
            catch (UpdateFailedException) {
                MessageBox.Show("You didn't update any of information!\n(Appointment remains the same)");
                EventBus.FireEvent("PatientAppointments");
            }


            
        }
    }
}
