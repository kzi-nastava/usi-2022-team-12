using HealthInstitution.Model.doctor;

namespace HealthInstitution.Model.user
{
    public class Doctor : User
    {
        #region Attributes

        private DoctorSpecialization _specialization;
        public DoctorSpecialization Specialization { get => _specialization; set => OnPropertyChanged(ref _specialization, value); }

        #endregion
    }
}