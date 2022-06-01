using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using HealthInstitution.Utility;

namespace HealthInstitution.Dialogs.Custom
{
    public class DoctorSpecializationSelectViewModel : DialogReturnViewModelBase<DoctorSpecializationSelectViewModel, DoctorSpecialization>
    {
        #region Properties

        private IEnumerable<DoctorSpecialization> _doctorSpecializations;
        public IEnumerable<DoctorSpecialization> DoctorSpecializations
        {
            get { return _doctorSpecializations; }
            set { OnPropertyChanged(ref _doctorSpecializations, value); }
        }

        private DoctorSpecialization? _selectedSpecialization;
        public DoctorSpecialization? SelectedSpecialization
        {
            get { return _selectedSpecialization; }
            set { OnPropertyChanged(ref _selectedSpecialization, value); }
        }

        #endregion

        #region Services

        public ICommand Select { get; set; }

        #endregion

        public DoctorSpecializationSelectViewModel() :
            base("Doctor specialization select", 500, 250)
        {
            Select = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, (DoctorSpecialization)SelectedSpecialization),
                (IDialogWindow w) => { return SelectedSpecialization != null; });

            FetchSpecializations();
        }

        public void FetchSpecializations()
        {
            _doctorSpecializations = (DoctorSpecialization[])Enum.GetValues(typeof(DoctorSpecialization));
        }
    }
}
