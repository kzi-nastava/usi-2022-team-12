using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.GUI.Features.UsersManagement.Dialog;
using HealthInstitution.GUI.Utility.Dialog.Service;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.NotificationManagement.Repository;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.NotificationManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Service;

namespace HealthInstitution.GUI.Features.AppointmentScheduling.Dialog
{
    public class DelayAppointmentViewModel : DialogViewModelBase<HandlePatientViewModel>
    {
        #region Properties

        private IList<Tuple<Appointment, DateTime>> _delayAppointments;
        public IList<Tuple<Appointment, DateTime>> DelayAppointments
        {
            get { return _delayAppointments; }
            set { OnPropertyChanged(ref _delayAppointments, value); }
        }

        private Tuple<Appointment, DateTime>? _selectedDelayAppointment;
        public Tuple<Appointment, DateTime>? SelectedDelayAppointment
        {
            get { return _selectedDelayAppointment; }
            set { OnPropertyChanged(ref _selectedDelayAppointment, value); }
        }

        private Guid _patientId;

        #endregion

        #region Commands

        public ICommand DelayAppointment { get; private set; }

        #endregion

        #region Services

        private readonly IPatientService _patientService;

        private readonly IAppointmentService _appointmentService;

        private readonly IUserNotificationService _userNotificationService;

        private readonly IRoomService _roomService;

        #endregion

        public DelayAppointmentViewModel(IRoomService roomService, IAppointmentService appointmentService,
            IPatientService patientService, IUserNotificationService userNotificationService,
            IList<Tuple<Appointment, DateTime>> delayAppointments,
            Guid patientId) :
            base("Delay appointment", 900, 550)
        {
            _patientService = patientService;
            _delayAppointments = delayAppointments;
            _appointmentService = appointmentService;
            _patientId = patientId;
            _userNotificationService = userNotificationService;
            _roomService = roomService;

            DelayAppointment = new RelayCommand<IDialogWindow>(w =>
            {
                Patient urgentPatient = _patientService.Read(_patientId);

                Appointment appointmentToChange = _appointmentService.Read(_selectedDelayAppointment.Item1.Id);
                Room freeRoom = _roomService.FindFreeRoom(appointmentToChange.Room.RoomType, _selectedDelayAppointment.Item2, _selectedDelayAppointment.Item2.AddMinutes(15));

                Appointment newAppointment = new Appointment
                {
                    Doctor = appointmentToChange.Doctor,
                    Patient = appointmentToChange.Patient,
                    StartDate = _selectedDelayAppointment.Item2,
                    EndDate = _selectedDelayAppointment.Item2.AddMinutes(15),
                    Room = freeRoom,
                    AppointmentType = appointmentToChange.AppointmentType,
                    IsDone = false,
                    Anamnesis = null
                };

                _appointmentService.Create(newAppointment);

                appointmentToChange.Patient = urgentPatient;
                _appointmentService.Update(appointmentToChange);

                MakeNotifications(newAppointment, appointmentToChange.StartDate);
                MessageBox.Show("Appointment scheduled successfully");
                CloseDialogWithResult(w, null);
            },
                (w) => SelectedDelayAppointment != null);
        }

        public void MakeNotifications(Appointment newAppointment, DateTime oldTime)
        {
            string notificationMessage = "Your appointment has been delayed from " + oldTime.ToString() + " to " + newAppointment.StartDate.ToString();

            _userNotificationService.CreateNotification(newAppointment.Patient.Id, notificationMessage);
            _userNotificationService.CreateNotification(newAppointment.Doctor.Id, notificationMessage);
        }
    }
}
