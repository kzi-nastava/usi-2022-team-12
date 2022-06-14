using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Utility;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Repository
{
    public interface IReferralRepository : ICrudRepository<Referral>
    {
        IEnumerable<Referral> GetValidReferralsForPatient(Guid patientId);
        bool PatientHasValidReferral(Guid patientId);
    }
}
