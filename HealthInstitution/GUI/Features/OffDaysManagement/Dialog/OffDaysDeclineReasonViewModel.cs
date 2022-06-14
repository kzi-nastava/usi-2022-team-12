using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.GUI.Utility.Dialog.Service;
using HealthInstitution.Utility;

namespace HealthInstitution.GUI.Features.OffDaysManagement.Dialog
{
    public class OffDaysDeclineReasonViewModel : DialogReturnViewModelBase<OffDaysDeclineReasonViewModel, string>
    {
        #region Properties

        private string? _reason;

        public string? Reason
        {
            get { return _reason; }
            set { OnPropertyChanged(ref _reason, value); }
        }

        #endregion

        #region Commands

        public ICommand Confirm { get; set; }

        #endregion

        public OffDaysDeclineReasonViewModel() :
            base("Decline reason", 500, 250)
        {
            Confirm = new RelayCommand<IDialogWindow>(w => { CloseDialogWithResult(w, Reason); },
                w => !string.IsNullOrEmpty(Reason));

        }
    }
}
