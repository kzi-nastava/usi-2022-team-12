namespace HealthInstitution.Core.Features.UsersManagement.Model
{
    public class Doctor : User
    {
        #region Attributes

        private DoctorSpecialization _specialization;
        public DoctorSpecialization Specialization { get => _specialization; set => OnPropertyChanged(ref _specialization, value); }

        #endregion

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}