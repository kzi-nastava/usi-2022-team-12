using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.MedicalRecordManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Service
{
    public class MedicalRecordService : IMedicalRecordService
    {
        IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        #region CRUD methods

        public IEnumerable<MedicalRecord> ReadAll()
        {
            return _medicalRecordRepository.ReadAll();
        }

        public MedicalRecord Read(Guid recordId)
        {
            return _medicalRecordRepository.Read(recordId);
        }

        public MedicalRecord Create(MedicalRecord newRecord)
        {
            return _medicalRecordRepository.Create(newRecord);
        }

        public MedicalRecord Update(MedicalRecord recordToUpdate)
        {
            return _medicalRecordRepository.Update(recordToUpdate);
        }

        public MedicalRecord Delete(Guid recordId)
        {
            return _medicalRecordRepository.Delete(recordId);
        }

        #endregion

        public MedicalRecord GetMedicalRecordForPatient(Patient patient)
        {
            return _medicalRecordRepository.GetMedicalRecordForPatient(patient);
        }
    }
}
