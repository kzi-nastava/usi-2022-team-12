using System;
using System.Windows;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Features.OperationsAndExaminations.Services;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.AppointmentScheduling.Dialog;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.SecretaryCMD
{
    public class UseReferralCommand : CommandBase
    {
        private readonly ReferralCardViewModel _referralCardVM;
        private readonly ReferralUsageViewModel _referralUsageVM;

        private readonly IReferralService _referralService;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IRoomService _roomService;

        public UseReferralCommand(ReferralUsageViewModel referralUsageVM, ReferralCardViewModel referralCardVM,
            IReferralService referralService, IAppointmentRepository appointmentRepository, IDoctorService doctorService,
            IPatientService patientService, IRoomService roomService)
        {
            _referralCardVM = referralCardVM;
            _referralUsageVM = referralUsageVM;
            _referralService = referralService;
            _appointmentRepository = appointmentRepository;
            _doctorService = doctorService;
            _patientService = patientService;
            _roomService = roomService;
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
            Patient patient = _patientService.Read(_referralUsageVM.PatientId);

            Room freeRoom;
            if (_referralCardVM.AppointmentType == AppointmentType.Regular)
            {
                freeRoom = _roomService.FindFreeRoom(RoomType.ExaminationRoom, finalAppointmentTime, finalAppointmentTime.AddMinutes(15));
            }
            else
            {
                freeRoom = _roomService.FindFreeRoom(RoomType.OperationRoom, finalAppointmentTime, finalAppointmentTime.AddMinutes(15));
            }

            if (!_doctorService.IsDoctorAvailable(doctor, finalAppointmentTime, finalAppointmentTime.AddMinutes(15)))
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

                _appointmentRepository.Create(newAppointment);

                MessageBox.Show("Appointment successfully created.");

                _referralUsageVM.UpdatePage(0);
            }
        }
    }
}
