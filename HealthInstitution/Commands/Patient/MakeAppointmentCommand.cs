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
    public class MakeAppointmentCommand : CommandBase
    {
        private readonly AppointmentCreationViewModel? _viewModel;
        public MakeAppointmentCommand(AppointmentCreationViewModel viewModel) {
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

        public override void Execute(object? parameter) {
            DateTime startTime = _viewModel.Date.AddHours(Int32.Parse(_viewModel.Hours)).AddMinutes(Int32.Parse(_viewModel.Minutes));
            DateTime endTime = startTime.AddMinutes(15);
            var doctorAppointments = _viewModel._appointmentService.ReadDoctorAppointemnts(_viewModel.SelectedDoctor, startTime, endTime);
            var appointmentsInInterval = _viewModel._appointmentService.ReadAppointemntsInInterval(startTime, endTime);
            var examinationRooms = _viewModel._roomService.ReadRoomsWithType(RoomType.ExaminationRoom);


            if (doctorAppointments.Count() != 0) {
                MessageBox.Show("Selected doctor is busy at selected time!");
                return;
            }

            Room emptyRoom = null;
            foreach (var room in examinationRooms)
            {
                emptyRoom = room;
                foreach (var appointment in appointmentsInInterval)
                {
                    if (room == appointment.Room) {
                        emptyRoom = null;
                        break;
                    }
                }
                if (emptyRoom != null) {
                    break;
                }
            }

            if (emptyRoom == null) {
                MessageBox.Show("All rooms are busy at selected time!");
                return;
            }

            Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
            var an = new Anamnesis("This is anamnesis");
            var app = new Appointment(_viewModel.SelectedDoctor, pt, startTime, emptyRoom, an);
            _viewModel._appointmentService.Create(app);
            Activity act = new Activity(DateTime.Now, ActivityType.Create);
            pt.AddActivity(act);
            MessageBox.Show("Appointment created successfully!");
            EventBus.FireEvent("PatientAppointments");

            var activities = pt.Activities.ToList<Activity>().FindAll(act => act.ActivityType == ActivityType.Delete || act.ActivityType == ActivityType.Delete);
            
        }
    }
}
