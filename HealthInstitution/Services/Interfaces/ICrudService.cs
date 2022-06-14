using System;
using System.Collections.Generic;
using HealthInstitution.Model;

namespace HealthInstitution.Services.Interfaces
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
