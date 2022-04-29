﻿using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IMedicalRecordService : ICrudService<MedicalRecord>
    {
        MedicalRecord GetMedicalRecordForPatient(Patient patient);
    }
}
