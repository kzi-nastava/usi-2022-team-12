using HealthInstitution.Exceptions;
using HealthInstitution.Model;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands
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
            if (e.PropertyName == nameof(_viewModel.Date) || e.PropertyName == nameof(_viewModel.Hours) || e.PropertyName == nameof(_viewModel.Minutes) || e.PropertyName == nameof(_viewModel.SelectedDoctor))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.Date <= DateTime.Now) && !string.IsNullOrEmpty(_viewModel.Hours) && !string.IsNullOrEmpty(_viewModel.Minutes)
                && !(_viewModel.SelectedDoctor == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            DateTime startTime = _viewModel.Date.AddHours(Int32.Parse(_viewModel.Hours)).AddMinutes(Int32.Parse(_viewModel.Minutes));
            DateTime endTime = startTime.AddMinutes(15);

            try
            {
                Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
                var updated = _viewModel.appointmentService.updateAppointment(_viewModel.ChosenAppointment, pt, _viewModel.SelectedDoctor, startTime, endTime);

                if (updated)
                {
                    MessageBox.Show("Appointment updated successfully!");

                }
                else {
                    MessageBox.Show("Request for appointment update created successfully!\nPlease wait for secretary to review it.");
                }

                Activity act = new Activity(pt, DateTime.Now, ActivityType.Update);
                _viewModel.activityService.Create(act);

                var activityCount = _viewModel.activityService.ReadPatientUpdateOrRemoveActivity(pt, 30).ToList<Activity>().Count;
                if (activityCount >= 5)
                {
                    pt.IsBlocked = true;
                    _viewModel.patientService.Update(pt);
                    MessageBox.Show("Your profile has been blocked!\n(Too many appointments removed or updated)");
                    EventBus.FireEvent("BackToLogin");
                }
                else
                {
                    EventBus.FireEvent("PatientAppointments");
                }
            }
            catch (DoctorBusyException ex)
            {
                MessageBox.Show("Selected doctor is busy at selected time!");
            }
            catch (RoomBusyException ex)
            {
                MessageBox.Show("All rooms are busy at selected time!");
            }
            catch (UpdateFailedException ex) {
                MessageBox.Show("You didn't update any of information!\n(Appointment remains the same)");
                EventBus.FireEvent("PatientAppointments");
            }


            
        }
    }
}
