using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Model.patient;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Services.Implementation
{
    public class PatientService : UserService<Patient>, IPatientService
    {
        private readonly IActivityService _activityService;
        public PatientService(DatabaseContext context, IActivityService activityService) :
            base(context)
        {
            _activityService = activityService;
        }


        public IEnumerable<Patient> ReadAllValidPatients()
        {
            return _entities.Where(p => p.IsBlocked == false).ToList();
        }

        public IEnumerable<Patient> ReadAllBlockedPatients()
        {
            return _entities.Where(p => p.IsBlocked == true).ToList();
        }

        public IEnumerable<Patient> FilterPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.EmailAddress.ToLower().Contains(searchText) || p.FirstName.ToLower().Contains(searchText)
           || p.LastName.ToLower().Contains(searchText) || p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public IEnumerable<Patient> FilterValidPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.IsBlocked == false)
                            .Where(p => p.EmailAddress.ToLower().Contains(searchText) || p.FirstName.ToLower().Contains(searchText)
            || p.LastName.ToLower().Contains(searchText) || p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public IEnumerable<Patient> FilterBlockedPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.IsBlocked == true)
                            .Where(p => p.EmailAddress.ToLower().Contains(searchText) || p.FirstName.ToLower().Contains(searchText)
            || p.LastName.ToLower().Contains(searchText) || p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public void BlockPatient(Patient patientToBlock)
        {
            patientToBlock.IsBlocked = true;
            Update(patientToBlock);
        }

        public void UnblockPatient(Patient patientToUnblock)
        {
            patientToUnblock.IsBlocked = false;
            Update(patientToUnblock);

            var updateOrRemoveAct = _activityService.ReadPatientUpdateOrRemoveActivity(patientToUnblock, 30).ToList<Activity>();
            var makeAct = _activityService.ReadPatientMakeActivity(patientToUnblock, 30).ToList<Activity>();
            foreach (var act in updateOrRemoveAct) {
                _activityService.Delete(act.Id);
            }
            foreach (var act in makeAct)
            {
                _activityService.Delete(act.Id);
            }
        }
    }
}
