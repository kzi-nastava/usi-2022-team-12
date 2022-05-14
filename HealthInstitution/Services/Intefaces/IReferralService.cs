using HealthInstitution.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IReferralService : ICrudService<Referral>
    {
        public IEnumerable<Referral> GetValidReferralsForPatient(Guid patientId);
        bool PatientHasValidReferral(Guid patientId);
    }
}
