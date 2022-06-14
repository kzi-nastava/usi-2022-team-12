using HealthInstitution.Dialogs.Service;
using HealthInstitution.Utility;
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.Dialogs.Custom
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
