using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.Core.Features.OffDaysManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Services.Interfaces;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.OffDaysManagement
{
    public class DoctorOffDaysViewModel : ViewModelBase
    {
        #region Atributes
        private IOffDaysRequestService _offDaysRequestService;
        private Doctor _doctor;
        #endregion

        #region Properties
        public IOffDaysRequestService OffDaysRequestService => _offDaysRequestService;
        public Doctor Doctor => _doctor;
        #endregion

        #region Collections
        private ObservableCollection<OffDaysRequest> _offDays;
        public IEnumerable<OffDaysRequest> OffDays
        {
            get => _offDays;
            set
            {
                _offDays = new ObservableCollection<OffDaysRequest>();
                foreach (OffDaysRequest odr in value)
                {
                    _offDays.Add(odr);
                }
                OnPropertyChanged(nameof(OffDays));
            }
        }
        #endregion

        #region Commands
        public ICommand SendRequestCommand { get; }
        #endregion
        public DoctorOffDaysViewModel(IOffDaysRequestService offDaysRequestService)
        {

            _offDaysRequestService = offDaysRequestService;
            _doctor = GlobalStore.ReadObject<Doctor>("LoggedUser");
            OffDays = offDaysRequestService.GetOffDaysForDoctor(_doctor);
            SendRequestCommand = new NavigateOffDaysRequestCreationCommand();
        }
    }
}
