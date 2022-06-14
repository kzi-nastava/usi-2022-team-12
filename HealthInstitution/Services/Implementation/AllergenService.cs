using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Model.appointment;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Services.Implementation
{
    public class AllergenService : CrudService<Allergen>, IAllergenService
    {
        public AllergenService(DatabaseContext context) : base(context) { }
    }
}
