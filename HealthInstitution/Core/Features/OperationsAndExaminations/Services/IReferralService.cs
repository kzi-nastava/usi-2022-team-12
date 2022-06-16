using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Services
{
    public interface IReferralService : ICrudService<Referral>
    {
        public IEnumerable<Referral> GetValidReferralsForPatient(Guid patientId);

        public bool PatientHasValidReferral(Guid patientId);
    }
}
