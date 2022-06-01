using HealthInstitution.Commands.Secretary;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Validation;
using HealthInstitution.ViewModel;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace HealthInstitution.Dialogs.Custom
{
    public class HandlePatientViewModel : DialogViewModelBase<HandlePatientViewModel>
    {

        #region Properties

        private string _emailAddress;
        [ValidationField]
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; OnPropertyChanged(nameof(EmailAddress)); OnPropertyChanged(nameof(CanExecute)); }
        }

        private string _firstName;
        [ValidationField]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); OnPropertyChanged(nameof(CanExecute)); }
        }

        private string _lastName;
        [ValidationField]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); OnPropertyChanged(nameof(CanExecute)); }
        }

        private string _password;
        [ValidationField]
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); OnPropertyChanged(nameof(CanExecute)); }
        }

        private string _confirmPassword;
        [ValidationField]
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); OnPropertyChanged(nameof(CanExecute)); }
        }

        private DateTime _dateOfBirth = DateTime.Now;
        [ValidationField]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); OnPropertyChanged(nameof(CanExecute)); }
        }

        #endregion

        #region Additional properties

        public bool ReadOnlyEmailAddress { get; private set; }

        private Guid _patientId;

        public string ActionButtonName { get; private set; }

        #endregion

        #region Error message view models

        private static readonly Regex _emailregex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

        public ErrorMessageViewModel FirstNameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel LastNameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel EmailAddressError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel PasswordError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel ConfirmPasswordError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel DateOfBirthError { get; private set; } = new ErrorMessageViewModel();

        public bool CanExecute => IsValid();

        #endregion

        #region Commands

        public ICommand HandlePatient { get; private set; }

        #endregion

        #region Services

        private readonly IDialogService _dialogService;

        private readonly IMedicalRecordService _medicalRecordService;

        private readonly IPatientService _patientService;

        #endregion

        public HandlePatientViewModel(IDialogService dialogService, IPatientService patientService,
            IMedicalRecordService medicalRecordService,
            SecretaryPatientCRUDViewModel secretartyPatientCRUDVM, Guid patientId) :
            base("Add patient", 700, 550)
        {
            _dialogService = dialogService;
            _patientService = patientService;
            _medicalRecordService = medicalRecordService;
            _patientId = patientId;

            if (patientId != Guid.Empty)
            {
                FetchPatient();
                ActionButtonName = "Update";
                ReadOnlyEmailAddress = true;
            }
            else
            {
                ActionButtonName = "Add";
                ReadOnlyEmailAddress = false;
            }

            HandlePatient = new HandlePatientCommand(this, patientService, medicalRecordService, secretartyPatientCRUDVM, patientId);
        }


        public void ResetFields()
        {
            EmailAddress = null;
            FirstName = null;
            LastName = null;
            Password = null;
            ConfirmPassword = null;
            DateOfBirth = DateTime.Now;
            ResetDirtyValues();
            IsValid();
        }

        public void FetchPatient()
        {
            var patient = _patientService.Read(_patientId);

            EmailAddress = patient.EmailAddress;
            FirstName = patient.FirstName;
            LastName = patient.LastName;
            Password = patient.Password;
            ConfirmPassword = patient.Password;
            DateOfBirth = patient.DateOfBirth;

        }

        public bool IsValid()
        {
            bool valid = true;

            // Check if email is valid
            if (string.IsNullOrEmpty(EmailAddress) && IsDirty(nameof(EmailAddress)))
            {
                EmailAddressError.ErrorMessage = "Email cannot be empty.";
                valid = false;
            }
            else if (!string.IsNullOrEmpty(EmailAddress) && !_emailregex.IsMatch(EmailAddress))
            {
                EmailAddressError.ErrorMessage = "Email is not in correct format.";
                valid = false;
            }
            else
            {
                EmailAddressError.ErrorMessage = null;
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
