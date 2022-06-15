using System;
using System.Windows;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Features.OperationsAndExaminations.Repository;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.AppointmentScheduling.Dialog;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.SecretaryCMD
{
    public class UseReferralCommand : CommandBase
    {
        private readonly ReferralCardViewModel _referralCardVM;
        private readonly ReferralUsageViewModel _referralUsageVM;

        private readonly IReferralRepository _referralRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;        
        private readonly IRoomService _roomService;
        private readonly IDoctorService _doctorService;

        public UseReferralCommand(ReferralUsageViewModel referralUsageVM, ReferralCardViewModel referralCardVM,
            IReferralRepository referralRepository, IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository,
            IPatientRepository patientRepository, IRoomService roomService, IDoctorService doctorService)
        {
            _referralCardVM = referralCardVM;
            _referralUsageVM = referralUsageVM;
            _referralRepository = referralRepository;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _roomService = roomService;
            _doctorService = doctorService;
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

            Doctor doctor = _doctorRepository.Read(_referralCardVM.DoctorId);
            Patient patient = _patientRepository.Read(_referralUsageVM.PatientID);

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
                Referral referralToChange = _referralRepository.Read(_referralCardVM.ReferralId);
                referralToChange.IsUsed = true;
                _referralRepository.Update(referralToChange);

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

                MessageBox.Show("Appointment succesfully created.");

                _referralUsageVM.UpdatePage(0);
            }
        }
    }
}
