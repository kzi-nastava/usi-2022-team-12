using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Commands.Secretary
{
    public class UserReferralCommand : CommandBase
    {
        private readonly ReferralCardViewModel _referralCardVM;
        private readonly IAppointmentService _appointmentService;
        private readonly ReferralUsageViewModel _referralUsageVM;

        public UserReferralCommand(ReferralCardViewModel referralCardVM, IAppointmentService appointmentService,
            ReferralUsageViewModel referralUsageVM)
        {
            _referralCardVM = referralCardVM;
            _appointmentService = appointmentService;
            _referralUsageVM = referralUsageVM;
            _referralUsageVM.PropertyChanged += _referralUsageVM_PropertyChanged;
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
            return ReferralCardViewModel.CanExecute;
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
    }
}
