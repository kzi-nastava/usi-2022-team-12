using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IActivityRepository _activityRepository;
        public PatientService(IPatientRepository patientRepository, IActivityRepository activityRepository)
        {
            _patientRepository = patientRepository;
            _activityRepository = activityRepository;
        }

        public IEnumerable<Patient> ReadAllValidPatients()
        {
            return _patientRepository.ReadAll().Where(p => p.IsBlocked == false).ToList();
        }

        public IEnumerable<Patient> ReadAllBlockedPatients()
        {
            return _patientRepository.ReadAll().Where(p => p.IsBlocked == true).ToList();
        }

        public IEnumerable<Patient> FilterPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _patientRepository.ReadAll().Where(p => p.EmailAddress.ToLower().Contains(searchText) || p.FirstName.ToLower().Contains(searchText)
           || p.LastName.ToLower().Contains(searchText) || p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public IEnumerable<Patient> FilterValidPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _patientRepository.ReadAll().Where(p => p.IsBlocked == false)
                            .Where(p => p.EmailAddress.ToLower().Contains(searchText) || p.FirstName.ToLower().Contains(searchText)
            || p.LastName.ToLower().Contains(searchText) || p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public IEnumerable<Patient> FilterBlockedPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _patientRepository.ReadAll().Where(p => p.IsBlocked == true)
                            .Where(p => p.EmailAddress.ToLower().Contains(searchText) || p.FirstName.ToLower().Contains(searchText)
            || p.LastName.ToLower().Contains(searchText) || p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public void BlockPatient(Patient patientToBlock)
        {
            patientToBlock.IsBlocked = true;
            _patientRepository.Update(patientToBlock);
        }

        public void UnblockPatient(Patient patientToUnblock)
        {
            patientToUnblock.IsBlocked = false;
            _patientRepository.Update(patientToUnblock);

            var updateOrRemoveAct = _activityRepository.ReadPatientUpdateOrRemoveActivity(patientToUnblock, 30).ToList<Activity>();
            var makeAct = _activityRepository.ReadPatientMakeActivity(patientToUnblock, 30).ToList<Activity>();
            foreach (var act in updateOrRemoveAct)
            {
                _activityRepository.Delete(act.Id);
            }
            foreach (var act in makeAct)
            {
                _activityRepository.Delete(act.Id);
            }
        }
    }
}
