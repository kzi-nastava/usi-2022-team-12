using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Model.appointment;
using HealthInstitution.Model.user;

namespace HealthInstitution.Services.Implementation
{
    public class MedicalRecordService : CrudService<MedicalRecord>, IMedicalRecordService
    {
        public MedicalRecordService(DatabaseContext context) : base(context) { }

        public MedicalRecord GetMedicalRecordForPatient(Patient patient)
        {
            return _entities.Where(e => e.Patient == patient).FirstOrDefault();
        }
    }
}
