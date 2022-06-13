using HealthInstitution.Commands.doctor;
using HealthInstitution.Commands.doctor.Navigation;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel.doctor
{
    public class CreateOffDayRequestViewModel : ViewModelBase
    {
        #region Atributes
        private IOffDaysRequestService _offDaysRequestService;
        private string _reason;
        private DateTime _startDate;
        private DateTime _endDate;
        private bool _isUrgent;
        private Doctor _doctor;
        private DateTime _lowerBoundary;
        #endregion

        #region Properties
        public DateTime LowerBoundary => _lowerBoundary;
        public IOffDaysRequestService OffDaysRequestService => _offDaysRequestService;
        public string Reason
        {
            get => _reason;
            set
            {
                _reason = value;
                OnPropertyChanged(nameof(Reason));
            }
        }
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public bool IsUrgent
        {
            get => _isUrgent;
            set
            {
                _isUrgent = value;
                OnPropertyChanged(nameof(IsUrgent));
            }
        }
        public Doctor Doctor => _doctor;
        #endregion

        #region Commands
        public ICommand CancelCommand { get; }
        public ICommand SendRequestCommand { get; }
        #endregion
        public CreateOffDayRequestViewModel(IOffDaysRequestService offDaysRequestService)
        {
            _lowerBoundary = DateTime.Now.AddDays(2);
            _startDate = DateTime.Now.AddDays(2);
            _endDate = DateTime.Now.AddDays(2);
            _reason = "";
            _offDaysRequestService = offDaysRequestService;
            CancelCommand = new NavigateOffDaysCommand();
            SendRequestCommand = new SendRequestCommand(this);
            _doctor = GlobalStore.ReadObject<Doctor>("LoggedUser");
        }
    }
}
