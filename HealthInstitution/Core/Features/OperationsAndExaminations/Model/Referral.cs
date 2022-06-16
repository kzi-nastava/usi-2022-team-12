using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Model
{
    public class Referral : BaseObservableEntity
    {
        #region Attributes

        private Doctor? _doctor;
        public virtual Doctor Doctor { get => _doctor; set => OnPropertyChanged(ref _doctor, value); }

        private DoctorSpecialization? _specialization;
        public DoctorSpecialization? DoctorSpecialization { get => _specialization; set => OnPropertyChanged(ref _specialization, value); }

        private AppointmentType appointmentType;
        public AppointmentType AppointmentType { get => appointmentType; set => OnPropertyChanged(ref appointmentType, value); }

        private Patient _patient;
        public virtual Patient Patient { get => _patient; set => OnPropertyChanged(ref _patient, value); }

        private bool _isUsed;

        public bool IsUsed { get => _isUsed; set => OnPropertyChanged(ref _isUsed, value); }
        #endregion

        #region Constructor

        public Referral()
        {

        }

        #endregion
    }
}
