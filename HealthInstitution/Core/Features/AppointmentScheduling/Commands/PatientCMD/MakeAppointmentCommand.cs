using System;
using System.ComponentModel;
using System.Windows;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.AppointmentScheduling;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD
{
    public class MakeAppointmentCommand : CommandBase
    {
        private readonly AppointmentCreationViewModel? _viewModel;
        public MakeAppointmentCommand(AppointmentCreationViewModel viewModel)
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

            Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");

            try
            {
                _viewModel.SchedulingService.MakeAppointment(pt, _viewModel.SelectedDoctor, startDateTime, endDateTime, AppointmentType.Regular);
                Activity act = new Activity(pt, DateTime.Now, ActivityType.Create);
                _viewModel.ActivityService.Create(act);
                MessageBox.Show("Appointment created successfully!");
                var activityCount = _viewModel.ActivityService.GetNumberOfRecentCreateActivities(pt.Id, 30);
                if (activityCount > 8)
                {
                    pt.IsBlocked = true;
                    _viewModel.PatientService.Update(pt);
                    MessageBox.Show("Your profile has been blocked!\n(Too many appointments made)");
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
        }
    }
}
