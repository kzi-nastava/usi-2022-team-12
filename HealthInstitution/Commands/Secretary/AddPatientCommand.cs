using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace HealthInstitution.Commands.Secretary
{
    public class AddPatientCommand : CommandBase
    {
        private readonly AddPatientViewModel _addPatientVM;
        private readonly IPatientService _patientService;
        private readonly SecretaryPatientCRUDViewModel _secretaryPatientCRUDVM;

        public AddPatientCommand(AddPatientViewModel addPatientVM, IPatientService patientService, 
            SecretaryPatientCRUDViewModel secretaryPatientCRUDVM)
        {
            _addPatientVM = addPatientVM;
            _patientService = patientService;
            _addPatientVM.PropertyChanged += _addPatientVM_PropertyChanged;
            _secretaryPatientCRUDVM = secretaryPatientCRUDVM;
        }

        private void _addPatientVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddPatientViewModel.CanAdd))
            {
                OnCanExecuteChange(sender, e);
            }
        }

        public override bool CanExecute(object parameter)
        { 
            return _addPatientVM.CanAdd;
        }

        public override void Execute(object? parameter)
        {
            if (!_patientService.AlreadyInUse(_addPatientVM.Email))
            {
                var patientToRegister = new Patient
                {
                    EmailAddress = _addPatientVM.Email,
                    Password = _addPatientVM.Password,
                    FirstName = _addPatientVM.FirstName,
                    LastName = _addPatientVM.LastName,
                    DateOfBirth = _addPatientVM.DateOfBirth,
                    Role = Role.Patient,
                    IsBlocked = false,
                    Activities = new List<Activity>()
                };

                _patientService.Create(patientToRegister);
                _secretaryPatientCRUDVM.UpdatePage();
                _addPatientVM.ResetFields();
                _addPatientVM.CloseDialogWithResult((IDialogWindow) parameter, null);
            }
            else
            {
                _addPatientVM.EmailError.ErrorMessage = "Email already in use.";
            }
        }


    }
}
