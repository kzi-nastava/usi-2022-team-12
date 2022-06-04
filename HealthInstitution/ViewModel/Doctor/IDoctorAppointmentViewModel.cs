namespace HealthInstitution.ViewModel.doctor
{
    public interface IDoctorAppointmentViewModel
    {
        public void UpdateData(string prefix);
        public string SearchText { get; set; }
    }
}
