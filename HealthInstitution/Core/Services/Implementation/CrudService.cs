using HealthInstitution.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Services.Implementation
{
    public class CrudService<T> : ICrudService<T> where T : BaseObservableEntity
    {
        protected DatabaseContext _context;
        protected DbSet<T> _entities;

        public CrudService(DatabaseContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual IEnumerable<T> ReadAll()
        {
            return _entities.ToList();
        }

        public virtual T Read(Guid id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public virtual T Create(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public virtual T Update(T entity)
        {
            var entityToUpdate = Read(entity.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }

            return entityToUpdate;
        }

        public virtual T Delete(Guid id)
        {
            var entityToDelete = Read(id);
            if (entityToDelete != null)
            {
                _context.Remove(entityToDelete);
                _context.SaveChanges();
            }

            return entityToDelete;
        }
    }
}
