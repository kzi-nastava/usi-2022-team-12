using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.AppointmentScheduling.Dialog;
using HealthInstitution.GUI.Utility.Dialog.Service;
using System.Windows.Input;

namespace HealthInstitution.GUI.Features.EquipmentManagement.Dialog
{
    public class QuantitySelectViewModel : DialogReturnViewModelBase<DoctorSpecializationSelectViewModel, int>
    {
        #region Properties

        private string? _quantity;
        public string? Quantity
        {
            get { return _quantity; }
            set { OnPropertyChanged(ref _quantity, value); }
        }

        #endregion

        #region Commands

        public ICommand Confirm { get; set; }

        #endregion

        public QuantitySelectViewModel() :
            base("QuantitySelect", 500, 250)
        {
            Confirm = new RelayCommand<IDialogWindow>(w =>
            {
                int intQuantity;
                bool isNumeric = int.TryParse(Quantity, out intQuantity);
                CloseDialogWithResult(w, intQuantity);
            },

            (IDialogWindow w) =>
            {
                if (string.IsNullOrEmpty(Quantity))
                    return false;

                int intQuantity;
                bool isNumeric = int.TryParse(Quantity, out intQuantity);

                if (!isNumeric)
                    return false;

                if (intQuantity > 0)
                    return true;

                return false;
            });
        }
    }
}
