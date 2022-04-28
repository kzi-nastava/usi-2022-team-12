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

            //doctors availabilty check
            var doctorAvailability = _viewModel._appointmentService.IsDoctorAvailableForUpdate(_viewModel.SelectedDoctor, startTime, endTime, _viewModel.ChosenAppointment);
            if (!doctorAvailability)
            {
                MessageBox.Show("Selected doctor is busy at selected time!");
                return;
            }

            doctorAvailability = _viewModel._appointmentUpdateRequestService.IsDoctorAvailable(_viewModel.SelectedDoctor, startTime, endTime);
            if (!doctorAvailability)
            {
                MessageBox.Show("Selected doctor may be busy at selected time!");
                return;
            }

            //rooms availabilty check
            var examinationRooms = _viewModel._roomService.ReadRoomsWithType(RoomType.ExaminationRoom);
            Room emptyRoom = null;
            foreach (var room in examinationRooms)
            {
                var available = _viewModel._appointmentService.IsRoomAvailableForUpdate(room, startTime, endTime, _viewModel.ChosenAppointment);
                if (available)
                {
                    emptyRoom = room;
                    break;
                }
            }

            if (emptyRoom == null)
            {
                MessageBox.Show("All rooms are busy at selected time!");
                return;
            }

            if (_viewModel.ChosenAppointment.StartDate == startTime && _viewModel.ChosenAppointment.EndDate == endTime
                && _viewModel.ChosenAppointment.Doctor == _viewModel.SelectedDoctor) 
            {
                MessageBox.Show("You didn't update any of information!\n(Appointment remains the same)");
                EventBus.FireEvent("PatientAppointments");
                return;
            }


            Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
            if (DateTime.Now.AddDays(2) > _viewModel.ChosenAppointment.StartDate)
            {
                AppointmentUpdateRequest appointmentRequest = new AppointmentUpdateRequest { Patient = pt, Appointment = _viewModel.ChosenAppointment, 
                    ActivityType = ActivityType.Update , Status = Status.Pending, StartDate = startTime, EndDate = endTime, Doctor = _viewModel.SelectedDoctor};
                _viewModel._appointmentUpdateRequestService.Create(appointmentRequest);
                MessageBox.Show("Request for appointment update created successfully!\nPlease wait for secretary to review it.");

                Activity act = new Activity(pt, DateTime.Now, ActivityType.Update);
                _viewModel._activityService.Create(act);
                
                var activityCount = _viewModel._activityService.ReadPatientUpdateOrRemoveActivity(pt, 30).ToList<Activity>().Count;
                if (activityCount >= 5)
                {
                    pt.IsBlocked = true;
                    _viewModel._patientService.Update(pt);
                    MessageBox.Show("Your profile has been blocked!\n(Too many appointments removed or updated)");
                    EventBus.FireEvent("BackToLogin");
                }
                else
                {
                    EventBus.FireEvent("PatientAppointments");
                }
            }
            else 
            {
                _viewModel.ChosenAppointment.StartDate = startTime;
                _viewModel.ChosenAppointment.EndDate = endTime;
                _viewModel.ChosenAppointment.Doctor = _viewModel.SelectedDoctor;
                _viewModel._appointmentService.Update(_viewModel.ChosenAppointment);
                MessageBox.Show("Appointment updated successfully!");

                Activity act = new Activity(pt, DateTime.Now, ActivityType.Update);
                _viewModel._activityService.Create(act);

                var activityCount = _viewModel._activityService.ReadPatientUpdateOrRemoveActivity(pt, 30).ToList<Activity>().Count;
                if (activityCount >= 5)
                {
                    pt.IsBlocked = true;
                    _viewModel._patientService.Update(pt);
                    MessageBox.Show("Your profile has been blocked!\n(Too many appointments removed or updated)");
                    EventBus.FireEvent("BackToLogin");
                }
                else
                {
                    EventBus.FireEvent("PatientAppointments");
                }
            }
        }
    }
}
