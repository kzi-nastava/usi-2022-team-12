using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Features.OperationsAndExaminations.Repository;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Services
{
    public class ReferralService : IReferralService
    {
        private readonly IReferralRepository _referralRepository;

        public ReferralService(IReferralRepository referralRepository)
        {
            _referralRepository = referralRepository;
        }

        #region CRUD methods

        public IEnumerable<Referral> ReadAll()
        {
            return _referralRepository.ReadAll();
        }

        public Referral Read(Guid referralId)
        {
            return _referralRepository.Read(referralId);
        }

        public Referral Create(Referral newReferral)
        {
            return _referralRepository.Create(newReferral);
        }

        public Referral Update(Referral referralToUpdate)
        {
            return _referralRepository.Update(referralToUpdate);
        }

        public Referral Delete(Guid referralId)
        {
            return _referralRepository.Delete(referralId);
        }

        #endregion

        public IEnumerable<Referral> GetValidReferralsForPatient(Guid patientId)
        {
            return _referralRepository.GetValidReferralsForPatient(patientId);
        }

        public bool PatientHasValidReferral(Guid patientId)
        {
            return _referralRepository.PatientHasValidReferral(patientId);
        }
    }
}
