﻿using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Repository
{
    public class ReferralRepository : CrudRepository<Referral>, IReferralRepository
    {
        public ReferralRepository(DatabaseContext context) : base(context)
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
            return _entities
                .Where(r => r.Patient.Id == patientId)
                .Count(r => r.IsUsed == false) != 0;
        }
    }
}
