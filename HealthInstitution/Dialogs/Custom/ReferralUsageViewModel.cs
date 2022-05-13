using HealthInstitution.Dialogs.DialogPagination;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Pagination;
using HealthInstitution.Pagination.Requests;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Validation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthInstitution.Dialogs.Custom
{
    public class ReferralCardViewModel : ValidationModel<ReferralCardViewModel>
    {
        #region properties

        public string DoctorEmailAddress { get; set; }
        public string DoctorFullName { get; set; }
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

    public class ReferralUsageViewModel : DialogPagingViewModelBase<ReferralUsageViewModel>
    {
        private readonly IReferralService _referralService;

        public ObservableCollection<ReferralCardViewModel> ReferralViewModels { get; private set; } = new ObservableCollection<ReferralCardViewModel>();

        public ReferralUsageViewModel(IReferralService referralService) : base("Referral overview", "", 1000, 750)
        {
            _referralService = referralService;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            ReferralViewModels.Clear();
            var page = PageExtensions.ToPage(_referralService.ReadAll(),
                new ReferralPage
                {
                    Page = pageNumber,
                    Size = Size,
                    Query = ""
                }
            );

            foreach (var entity in page.Entities)
            {
                var referralModel = new ReferralCardViewModel
                {
                    DoctorEmailAddress = entity.Doctor.EmailAddress,
                    DoctorFullName = entity.Doctor.FullName,
                    DateOfAppointment = DateTime.Today
                };
                ReferralViewModels.Add(referralModel);
            }
            OnPageFetched(page);
        }
    }
}
