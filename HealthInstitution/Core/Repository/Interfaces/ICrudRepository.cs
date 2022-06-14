using HealthInstitution.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Repository.Interfaces
{
    public interface ICrudRepository<T> where T : IBaseEntity
    {
        IEnumerable<T> ReadAll();

        T Read(Guid id);

        T Create(T entity);

        T Update(T entity);

        T Delete(Guid id);
    }
}
