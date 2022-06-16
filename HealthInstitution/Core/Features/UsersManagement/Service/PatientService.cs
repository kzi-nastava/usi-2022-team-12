using System;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        private readonly IActivityService _activityService;

        public PatientService(IPatientRepository patientRepository, IActivityService activityService)
        {
            _patientRepository = patientRepository;
            _activityService = activityService;
        }

        #region CRUD methods

        public IEnumerable<Patient> ReadAll()
        {
            return _patientRepository.ReadAll();
        }

        public Patient Read(Guid patientId)
        {
            return _patientRepository.Read(patientId);
        }

        public Patient Create(Patient newPatient)
        {
            return _patientRepository.Create(newPatient);
        }

        public Patient Update(Patient patientToUpdate)
        {
            return _patientRepository.Update(patientToUpdate);
        }

        public Patient Delete(Guid patientId)
        {
            return _patientRepository.Delete(patientId);
        }

        #endregion

        public IEnumerable<Patient> ReadAllValidPatients()
        {
            return _patientRepository.ReadAllValidPatients();
        }

        public IEnumerable<Patient> ReadAllBlockedPatients()
        {
            return _patientRepository.ReadAllBlockedPatients();
        }

        public IEnumerable<Patient> FilterPatientsBySearchText(string searchText)
        {
            return _patientRepository.FilterPatientsBySearchText(searchText);
        }

        public IEnumerable<Patient> FilterValidPatientsBySearchText(string searchText)
        {
            return _patientRepository.FilterValidPatientsBySearchText(searchText);
        }

        public IEnumerable<Patient> FilterBlockedPatientsBySearchText(string searchText)
        {
            return _patientRepository.FilterBlockedPatientsBySearchText(searchText);
        }

        public void BlockPatient(Patient patientToBlock, BlockType blockedBy)
        {
            patientToBlock.IsBlocked = true;
            patientToBlock.BlockType = blockedBy;
            _patientRepository.Update(patientToBlock);
        }

        public void UnblockPatient(Patient patientToUnblock)
        {

            patientToUnblock.IsBlocked = false;
            patientToUnblock.BlockType = BlockType.UNBLOCKED;
            _patientRepository.Update(patientToUnblock);
            _activityService.ResetActivity(patientToUnblock.Id);
        }
    }
}
