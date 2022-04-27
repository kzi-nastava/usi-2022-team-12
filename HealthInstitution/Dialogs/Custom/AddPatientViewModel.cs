using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Utility;
using HealthInstitution.Validation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.Dialogs.Custom
{
    public class AddPatientViewModel : DialogViewModelBase<AddPatientViewModel, Patient>
    {

        #region Properties

        private string _email;
        [ValidationField]
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); OnPropertyChanged(nameof(CanAdd)); }
        }

        private string _firstName;
        [ValidationField]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); OnPropertyChanged(nameof(CanAdd)); }
        }

        private string _lastName;
        [ValidationField]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); OnPropertyChanged(nameof(CanAdd)); }
        }

        private string _password;
        [ValidationField]
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); OnPropertyChanged(nameof(CanAdd)); }
        }

        private string _confirmPassword;
        [ValidationField]
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); OnPropertyChanged(nameof(CanAdd)); }
        }

        private DateTime _dateOfBirth = DateTime.Now;
        [ValidationField]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); OnPropertyChanged(nameof(CanAdd)); }
        }

        #endregion

        #region Error message view models

        public ErrorMessageViewModel FirstNameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel LastNameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel EmailError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel PasswordError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel ConfirmPasswordError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel DateOfBirthError { get; private set; } = new ErrorMessageViewModel();

        public bool CanAdd => IsValid();

        #endregion

        #region Commands

        public ICommand Add { get; private set; }

        public ICommand Close { get; private set; }

        #endregion

        #region Rules

        private readonly IDialogService _dialogService;

        #endregion

        public AddPatientViewModel(IDialogService dialogService) :
            base("Add patient", 650, 600)
        {
            _dialogService = dialogService;

            Add = new RelayCommand<IDialogWindow>(w =>
            {
                CloseDialogWithResult(w, new Patient
                {
                    EmailAddress = Email,
                    Password = Password,
                    FirstName = FirstName,
                    LastName = LastName,
                    Role = Role.Patient,
                    IsBlocked = false,
                    Activities = new List<Activity>()
                });
            });

            Close = new RelayCommand<IDialogWindow>(w =>  CloseDialogWithResult(w, null));
        }


        public bool IsValid()
        {
            MessageBox.Show("USP");
            bool valid = true;

            // Email
            if (string.IsNullOrEmpty(Email) && IsDirty(nameof(Email))) 
            {
                EmailError.ErrorMessage = "Email cannot be empty.";
                valid = false;
            } 
            else
            {
                EmailError.ErrorMessage = null;
            }

            // First name
            if (string.IsNullOrEmpty(FirstName) && IsDirty(nameof(FirstName))) 
            {
                FirstNameError.ErrorMessage = "First name cannot be empty.";
                valid = false;
            }
            else
            {
                FirstNameError.ErrorMessage = null;
            }

            // Last name
            if (string.IsNullOrEmpty(LastName) && IsDirty(nameof(LastName))) 
            {
                LastNameError.ErrorMessage = "Last name cannot be empty.";
                valid = false;
            }
            else
            {
                LastNameError.ErrorMessage = null;
            }

            // Password
            if (string.IsNullOrEmpty(Password) && IsDirty(nameof(Password))) 
            {
                PasswordError.ErrorMessage = "Password cannot be empty.";
                valid = false;
            }
            else
            {
                PasswordError.ErrorMessage = null;
            }

            // Confirm password
            if (ConfirmPassword != Password && IsDirty(nameof(ConfirmPassword))) 
            {
                ConfirmPasswordError.ErrorMessage = "Password and confirm password must match.";
                valid = false;
            }
            else
            {
                ConfirmPasswordError.ErrorMessage = null;
            }

            // Date of birth
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth > today.AddYears(-age)) 
            {
                age--;
            }
            if (age < 18 && IsDirty(nameof(DateOfBirth)))
            {
                DateOfBirthError.ErrorMessage = "User must be at least 18 years old in order to be registered.";
                valid = false;
            }
            else
            {
                DateOfBirthError.ErrorMessage = null;
            }

            return valid && AllDirty();
        }
    }
}
