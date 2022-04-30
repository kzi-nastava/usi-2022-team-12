﻿using HealthInstitution.Exceptions;
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
            if (e.PropertyName == nameof(_viewModel.StartDateTime) || e.PropertyName == nameof(_viewModel.SelectedDoctor))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.StartDateTime <= DateTime.Now) && !(_viewModel.SelectedDoctor == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            DateTime startDateTime = _viewModel.StartDateTime;
            DateTime endDateTime = startDateTime.AddMinutes(15);

            try
            {
                Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
                var updated = _viewModel.appointmentService.updateAppointment(_viewModel.ChosenAppointment, pt, _viewModel.SelectedDoctor, startDateTime, endDateTime);

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
