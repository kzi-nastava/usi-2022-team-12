using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface IReferralService : ICrudService<Referral>
    {
        public IEnumerable<Referral> GetValidReferralsForPatient(Guid patientId);
        bool PatientHasValidReferral(Guid patientId);
    }
}
