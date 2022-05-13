using HealthInstitution.Dialogs.Service;
using HealthInstitution.Validation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthInstitution.Dialogs.Custom
{
    public class ReferralCardViewModel : ValidationModel<ReferralCardViewModel>
    {
        #region properties

        public string DoctorEmail { get; set; }
        public string DoctorFullName { get; set; }
        public string PatientEmail { get; set; }
        public string PatientFullName { get; set; }
        [ValidationField]
        public DateTime DateOfAppointment { get; set; }
        public ICommand UseReferral { get; set; }

        #endregion

        #region errors

        public ErrorMessageViewModel DateOfAppointmentError { get; private set; } = new ErrorMessageViewModel();

        #endregion

        #region command utility

        public bool CanExecute => IsValid();

        public bool IsValid()
        {
            bool valid = true;

            // Date of appointment
            DateTime today = DateTime.Today;
            if (DateOfAppointment > today.AddDays(7) && IsDirty(nameof(DateOfAppointment)))
            {
                DateOfAppointmentError.ErrorMessage = "You exceeded scheduling limit of 7 days";
                valid = false;
            }
            if (DateOfAppointment < today.AddHours(2) && IsDirty(nameof(DateOfAppointment)))
            {
                DateOfAppointmentError.ErrorMessage = "Minimal scheduling limit is 2 hours.";
                valid = false;
            }
            else
            {
                DateOfAppointmentError.ErrorMessage = null;
            }

            return valid && AllDirty();
        }

        #endregion
    }

    public class ReferralUsageViewModel : DialogViewModelBase<ReferralUsageViewModel>
    {
        private readonly IReferralService _referralService;

        public ObservableCollection<ReferralCardViewModel> ReferralViewModels { get; private set; } = new ObservableCollection<ReferralCardViewModel>();

        public ReferralUsageViewModel()
        {

        }

        public void UpdatePage()
        {
            ReferralViewModels.Clear();
            var referrals = IReferralService.GetValidReferrals().toPage;

        }
    }
}
