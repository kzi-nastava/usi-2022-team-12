namespace HealthInstitution.Model
{
    public enum Status
    {
        Approved,   // medicine is approved by doctor
        Pending,    // medicine is waiting for rewiev
        Rejected,   // medicine is rejected by doctor
        Revision    // medicine is waiting for revision
    }
}
