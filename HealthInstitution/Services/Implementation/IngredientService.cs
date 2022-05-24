using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class IngredientService : CrudService<Ingredient>, IIngredientService
    {
        public IngredientService(DatabaseContext context) : base(context)
        {

        }

    }
}