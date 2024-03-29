﻿using System.Linq;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public class UserRepository<T> : CrudRepository<T>, IUserRepository<T> where T : User
    {
        public UserRepository(DatabaseContext context) : base(context)
        {

        }

        public User Authenticate(string email, string password)
        {
            return _entities.FirstOrDefault(u => u.EmailAddress == email && u.Password == password);
        }

        public bool AlreadyInUse(string email)
        {
            return _context.Users.Any(u => u.EmailAddress == email);
        }

        public bool IsEmailUsed(string email)
        {
            return _context.Users.Any(u => u.EmailAddress == email);
        }
    }
}
