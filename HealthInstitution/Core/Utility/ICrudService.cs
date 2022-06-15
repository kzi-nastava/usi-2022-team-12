using System;
using System.Collections.Generic;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Utility
{
    public interface ICrudService<T> where T : IBaseEntity
    {
        IEnumerable<T> ReadAll();

        T Read(Guid id);

        T Create(T entity);

        T Update(T entity);

        T Delete(Guid id);
    }
}
