namespace HealthInstitution.GUI.Features.AppointmentScheduling
{
    public interface IDoctorAppointmentViewModel
    {
        public void UpdateData(string prefix);
        public string SearchText { get; set; }
    }
}
