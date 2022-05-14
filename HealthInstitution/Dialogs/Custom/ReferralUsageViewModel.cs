using HealthInstitution.Commands.Secretary;
using HealthInstitution.Dialogs.DialogPagination;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Pagination;
using HealthInstitution.Pagination.Requests;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Validation;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.Dialogs.Custom
{
    public class ReferralCardViewModel : ValidationModel<ReferralCardViewModel>
    {
        #region properties

        public Guid ReferralId { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorEmailAddress { get; set; }
        public string DoctorFullName { get; set; }

        public AppointmentType AppointmentType { get; set; }

        private DateTime? _dateOfAppointment = DateTime.Today;
        [ValidationField]
        public DateTime? DateOfAppointment
        {
            get { return _dateOfAppointment; }
            set { _dateOfAppointment = value; OnPropertyChanged(nameof(DateOfAppointment)); OnPropertyChanged(nameof(CanExecute)); }
        }

        private DateTime? _timeOfAppointment = DateTime.Today;
        [ValidationField]
        public DateTime? TimeOfAppointment
        {
            get { return _timeOfAppointment; }
            set { _timeOfAppointment = value; OnPropertyChanged(nameof(TimeOfAppointment)); OnPropertyChanged(nameof(CanExecute)); }
        }

        public ICommand UseReferral { get; set; }

        #endregion

        #region errors

        public ErrorMessageViewModel DateOfAppointmentError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel AppointmentError { get; private set; } = new ErrorMessageViewModel();

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
            else if (DateOfAppointment < today.AddHours(2) && IsDirty(nameof(DateOfAppointment)))
            {
                DateOfAppointmentError.ErrorMessage = "Minimal scheduling limit is 2 hours.";
                valid = false;
            }
            else if (DateOfAppointment == null && IsDirty(nameof(DateOfAppointment)))
            {
                valid = false;
            }
            else
            {
                DateOfAppointmentError.ErrorMessage = null;
            }

            // Time of appointment
            if (TimeOfAppointment == null && IsDirty(nameof(TimeOfAppointment)))
            {
                valid = false;
            }

            return valid && AllDirty();
        }

        #endregion
    }

    public class ReferralUsageViewModel : DialogPagingViewModelBase<ReferralUsageViewModel>
    {
        public Guid PatientID;

        private readonly IReferralService _referralService;
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public ObservableCollection<ReferralCardViewModel> ReferralViewModels { get; private set; } = new ObservableCollection<ReferralCardViewModel>();

        public ReferralUsageViewModel(Guid patientId, IReferralService referralService, IAppointmentService appointmentService,
            IDoctorService doctorService, IPatientService patientService) : base("Referral overview", "", 1000, 750)
        {
            PatientID = patientId;
            _referralService = referralService;
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            ReferralViewModels.Clear();
            var page = PageExtensions.ToPage(_referralService.GetValidReferralsForPatient(PatientID),
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
                    ReferralId = entity.Id,
                    DoctorId = entity.Doctor.Id,
                    DoctorEmailAddress = entity.Doctor.EmailAddress,
                    DoctorFullName = entity.Doctor.FullName,
                    AppointmentType = entity.AppointmentType,
                };
                referralModel.UseReferral = new UseReferralCommand(this, referralModel, _referralService, _appointmentService, _doctorService, _patientService);
                ReferralViewModels.Add(referralModel);
            }
            OnPageFetched(page);
        }
    }
}
