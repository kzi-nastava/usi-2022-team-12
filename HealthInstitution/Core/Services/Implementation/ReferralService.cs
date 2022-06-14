using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class ReferralService : CrudService<Referral>, IReferralService
    {
        public ReferralService(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<Referral> GetValidReferralsForPatient(Guid patientId)
        {
            return _entities.Where(r => r.Patient.Id == patientId)
                            .Where(r => r.IsUsed == false)
                            .ToList();
        }

        public bool PatientHasValidReferral(Guid patientId)
        {
            return _entities.Where(r => r.Patient.Id == patientId)
                            .Where(r => r.IsUsed == false)
                            .Count() != 0;
        }
    }
}
