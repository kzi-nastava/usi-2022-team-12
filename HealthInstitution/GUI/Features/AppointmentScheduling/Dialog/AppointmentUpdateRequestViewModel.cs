using System;
using System.Windows.Input;
using HealthInstitution.GUI.Utility.Dialog.Service;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;

namespace HealthInstitution.GUI.Features.AppointmentScheduling.Dialog
{
    public class AppointmentUpdateRequestViewModel : DialogViewModelBase<AppointmentUpdateRequestViewModel>
    {
        private Guid _appointmentRequestId;

        #region Properties

        private string _patientEmailAddress;
        public string PatientEmailAddress
        {
            get { return _patientEmailAddress; }
            set { _patientEmailAddress = value; OnPropertyChanged(nameof(PatientEmailAddress)); }
        }

        private string _patientFullName;
        public string PatientFullName
        {
            get { return _patientFullName; }
            set { _patientFullName = value; OnPropertyChanged(nameof(PatientFullName)); }
        }

        private string _oldDoctorEmailAddress;
        public string OldDoctorEmailAddress
        {
            get { return _oldDoctorEmailAddress; }
            set { _oldDoctorEmailAddress = value; OnPropertyChanged(nameof(OldDoctorEmailAddress)); }
        }

        private string _newDoctorEmailAddress;
        public string NewDoctorEmailAddress
        {
            get { return _newDoctorEmailAddress; }
            set { _newDoctorEmailAddress = value; OnPropertyChanged(nameof(NewDoctorEmailAddress)); }
        }

        private string _oldDoctorFullName;
        public string OldDoctorFullName
        {
            get { return _oldDoctorFullName; }
            set { _oldDoctorFullName = value; OnPropertyChanged(nameof(OldDoctorFullName)); }
        }

        private string _newDoctorFullName;
        public string NewDoctorFullName
        {
            get { return _newDoctorFullName; }
            set { _newDoctorFullName = value; OnPropertyChanged(nameof(NewDoctorFullName)); }
        }

        private string _oldDoctorSpecialization;
        public string OldDoctorSpecialization
        {
            get { return _oldDoctorSpecialization; }
            set { _oldDoctorSpecialization = value; OnPropertyChanged(nameof(OldDoctorSpecialization)); }
        }

        private string _newDoctorSpecialization;
        public string NewDoctorSpecialization
        {
            get { return _newDoctorSpecialization; }
            set { _newDoctorSpecialization = value; OnPropertyChanged(nameof(NewDoctorSpecialization)); }
        }

        private DateTime _oldAppointmentStartTime;
        public DateTime OldAppointmentStartTime
        {
            get { return _oldAppointmentStartTime; }
            set { _oldAppointmentStartTime = value; OnPropertyChanged(nameof(OldAppointmentStartTime)); }
        }

        private DateTime _newAppointmentStartTime;
        public DateTime NewAppointmentStartTime
        {
            get { return _newAppointmentStartTime; }
            set { _newAppointmentStartTime = value; OnPropertyChanged(nameof(NewAppointmentStartTime)); }
        }

        private DateTime _oldAppointmentEndTime;
        public DateTime OldAppointmentEndTime
        {
            get { return _oldAppointmentEndTime; }
            set { _oldAppointmentEndTime = value; OnPropertyChanged(nameof(OldAppointmentEndTime)); }
        }

        private DateTime _newAppointmentEndTime;
        public DateTime NewAppointmentEndTime
        {
            get { return _newAppointmentEndTime; }
            set { _newAppointmentEndTime = value; OnPropertyChanged(nameof(NewAppointmentEndTime)); }
        }

        private string _oldRoomName;
        public string OldRoomName
        {
            get { return _oldRoomName; }
            set { _oldRoomName = value; OnPropertyChanged(nameof(OldRoomName)); }
        }

        private string _newRoomName;
        public string NewRoomName
        {
            get { return _newRoomName; }
            set { _newRoomName = value; OnPropertyChanged(nameof(NewRoomName)); }
        }

        private string _oldRoomType;
        public string OldRoomType
        {
            get { return _oldRoomType; }
            set { _oldRoomType = value; OnPropertyChanged(nameof(OldRoomType)); }
        }

        private string _newRoomType;
        public string NewRoomType
        {
            get { return _newRoomType; }
            set { _newRoomType = value; OnPropertyChanged(nameof(NewRoomType)); }
        }

        #endregion

        #region Services

        private IAppointmentUpdateRequestRepository _appointmentUpdateRequestRepository;

        #endregion

        #region Commands

        public ICommand Ok { get; private set; }

        #endregion

        public AppointmentUpdateRequestViewModel(IAppointmentUpdateRequestRepository appointmentUpdateRequestRepository, Guid appointmentRequestId) :
                base("Appointment request details", 800, 650)
        {
            _appointmentUpdateRequestRepository = appointmentUpdateRequestRepository;
            _appointmentRequestId = appointmentRequestId;

            FetchAppointmentUpdateRequest();

            Ok = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));

        }

        public void FetchAppointmentUpdateRequest()
        {
            var appointmentUpdateRequest = _appointmentUpdateRequestRepository.Read(_appointmentRequestId);

            PatientEmailAddress = appointmentUpdateRequest.Patient.EmailAddress;
            PatientFullName = appointmentUpdateRequest.Patient.FullName;

            // Old info
            OldDoctorEmailAddress = appointmentUpdateRequest.Appointment.Doctor.EmailAddress;
            OldDoctorFullName = appointmentUpdateRequest.Appointment.Doctor.FullName;
            OldDoctorSpecialization = appointmentUpdateRequest.Appointment.Doctor.Specialization.ToString();
            OldAppointmentStartTime = appointmentUpdateRequest.Appointment.StartDate;
            OldAppointmentEndTime = appointmentUpdateRequest.Appointment.EndDate;
            OldRoomName = appointmentUpdateRequest.Appointment.Room.Name;
            OldRoomType = appointmentUpdateRequest.Appointment.Room.RoomType.ToString();

            // New info
            NewDoctorEmailAddress = appointmentUpdateRequest.Doctor.EmailAddress;
            NewDoctorFullName = appointmentUpdateRequest.Doctor.FullName;
            NewDoctorSpecialization = appointmentUpdateRequest.Doctor.Specialization.ToString();
            NewAppointmentStartTime = appointmentUpdateRequest.StartDate;
            NewAppointmentEndTime = appointmentUpdateRequest.EndDate;
            NewRoomName = appointmentUpdateRequest.Room.Name;
            NewRoomType = appointmentUpdateRequest.Room.RoomType.ToString();
        }
    }
}
