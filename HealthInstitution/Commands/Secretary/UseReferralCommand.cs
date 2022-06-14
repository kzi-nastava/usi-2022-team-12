using System;
using System.Windows;
using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Model.appointment;
using HealthInstitution.Model.doctor;
using HealthInstitution.Model.room;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Commands.secretary
{
    public class UseReferralCommand : CommandBase
    {
        private readonly ReferralCardViewModel _referralCardVM;
        private readonly ReferralUsageViewModel _referralUsageVM;

        private readonly IReferralService _referralService;
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public UseReferralCommand(ReferralUsageViewModel referralUsageVM, ReferralCardViewModel referralCardVM,
            IReferralService referralService, IAppointmentService appointmentService, IDoctorService doctorService,
            IPatientService patientService)
        {
            _referralCardVM = referralCardVM;
            _referralUsageVM = referralUsageVM;
            _referralService = referralService;
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _referralCardVM.PropertyChanged += _referralUsageVM_PropertyChanged;
        }

        private void _referralUsageVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ReferralCardViewModel.CanExecute))
            {
                OnCanExecuteChange(sender, e);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _referralCardVM.CanExecute;
        }

        public override void Execute(object? parameter)
        {
            DateTime appointmentDate = (DateTime)_referralCardVM.DateOfAppointment;
            DateTime appointmentTime = (DateTime)_referralCardVM.TimeOfAppointment;
            DateTime finalAppointmentTime = appointmentDate.Add(appointmentTime.TimeOfDay);

            Doctor doctor = _doctorService.Read(_referralCardVM.DoctorId);
            Patient patient = _patientService.Read(_referralUsageVM.PatientID);

            Room freeRoom;
            if (_referralCardVM.AppointmentType == AppointmentType.Regular)
            {
                freeRoom = _appointmentService.FindFreeRoom(RoomType.ExaminationRoom, finalAppointmentTime, finalAppointmentTime.AddMinutes(15));
            }
            else
            {
                freeRoom = _appointmentService.FindFreeRoom(RoomType.OperationRoom, finalAppointmentTime, finalAppointmentTime.AddMinutes(15));
            }

            if (!_appointmentService.IsDoctorAvailable(doctor, finalAppointmentTime, finalAppointmentTime.AddMinutes(15)))
            {
                _referralCardVM.AppointmentError.ErrorMessage = "Doctor is not available at a given time.";
            }
            else if (freeRoom == null)
            {
                _referralCardVM.AppointmentError.ErrorMessage = "There is no available room at a given time.";
            }
            else
            {
                Referral referralToChange = _referralService.Read(_referralCardVM.ReferralId);
                referralToChange.IsUsed = true;
                _referralService.Update(referralToChange);

                Appointment newAppointment = new Appointment
                {
                    Doctor = doctor,
                    Patient = patient,
                    StartDate = finalAppointmentTime,
                    EndDate = finalAppointmentTime.AddMinutes(15),
                    Room = freeRoom,
                    AppointmentType = _referralCardVM.AppointmentType,
                    IsDone = false,
                    Anamnesis = null
                };

                _appointmentService.Create(newAppointment);

                MessageBox.Show("Appointment succesfully created.");

                _referralUsageVM.UpdatePage(0);
            }
        }
    }
}
