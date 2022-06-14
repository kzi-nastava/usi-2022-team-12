using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;
using System;

namespace HealthInstitution.Core.Features.NotificationManagement.Model
{
    public class PrescribedMedicineNotification : BaseObservableEntity
    {
        private PrescribedMedicine _prescribedMedicine;
        public virtual PrescribedMedicine PrescribedMedicine { get => _prescribedMedicine; set => OnPropertyChanged(ref _prescribedMedicine, value); }

        private DateTime _triggerTime;
        public DateTime TriggerTime { get => _triggerTime; set => OnPropertyChanged(ref _triggerTime, value); }

        private bool _triggered;
        public bool Triggered { get => _triggered; set => OnPropertyChanged(ref _triggered, value); }

        public PrescribedMedicineNotification()
        {

        }
    }
}
