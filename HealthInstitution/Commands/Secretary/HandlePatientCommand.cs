using System;
using System.Collections.Generic;
using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model.appointment;
using HealthInstitution.Model.patient;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.ViewModel.secretary;

namespace HealthInstitution.Commands.secretary
{
    public class HandlePatientCommand : CommandBase
    {
        private readonly HandlePatientViewModel _handlePatientVM;
        private readonly IPatientService _patientService;
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly SecretaryPatientCRUDViewModel _secretaryPatientCRUDVM;

        private Guid _patientId;

        public HandlePatientCommand(HandlePatientViewModel handlePatientVM, IPatientService patientService,
            IMedicalRecordService medicalRecordService,
            SecretaryPatientCRUDViewModel secretaryPatientCRUDVM, Guid patientId)
        {
            _handlePatientVM = handlePatientVM;
            _patientService = patientService;
            _medicalRecordService = medicalRecordService;
            _handlePatientVM.PropertyChanged += _addPatientVM_PropertyChanged;
            _secretaryPatientCRUDVM = secretaryPatientCRUDVM;
            _patientId = patientId;
        }

        private void _addPatientVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HandlePatientViewModel.CanExecute))
            {
                OnCanExecuteChange(sender, e);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _handlePatientVM.CanExecute;
        }

        public override void Execute(object? parameter)
        {
            if (_patientId == Guid.Empty)
            {
                if (!_patientService.AlreadyInUse(_handlePatientVM.EmailAddress))
                {
                    HandleAction(parameter, AddPatient);
                }
                else
                {
                    _handlePatientVM.EmailAddressError.ErrorMessage = "Email already in use.";
                }
            }
            else
            {
                HandleAction(parameter, UpdatePatient);
            }
        }

        public void HandleAction(object parameter, Action action)
        {
            action();
            _secretaryPatientCRUDVM.UpdatePage();
            _handlePatientVM.ResetFields();
            _handlePatientVM.CloseDialogWithResult((IDialogWindow)parameter, null);
        }

        public void AddPatient()
        {
            var patientToRegister = new Patient
            {
                EmailAddress = _handlePatientVM.EmailAddress,
                Password = _handlePatientVM.Password,
                FirstName = _handlePatientVM.FirstName,
                LastName = _handlePatientVM.LastName,
                DateOfBirth = _handlePatientVM.DateOfBirth,
                Role = Role.Patient,
                IsBlocked = false,
                Activities = new List<Activity>()
            };

            _patientService.Create(patientToRegister);

            var medicalRecord = new MedicalRecord { Height = 0, Weight = 0, IllnessHistory = new List<Illness>(), Allergens = new List<Allergen>(), Patient = patientToRegister };

            _medicalRecordService.Create(medicalRecord);
        }

        public void UpdatePatient()
        {
            var patientToUpdate = _patientService.Read(_patientId);

            patientToUpdate.EmailAddress = _handlePatientVM.EmailAddress;
            patientToUpdate.Password = _handlePatientVM.Password;
            patientToUpdate.FirstName = _handlePatientVM.FirstName;
            patientToUpdate.LastName = _handlePatientVM.LastName;
            patientToUpdate.DateOfBirth = _handlePatientVM.DateOfBirth;

            _patientService.Update(patientToUpdate);
        }
    }
}
