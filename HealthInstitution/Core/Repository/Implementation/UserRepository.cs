using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Repository.Interfaces;
using HealthInstitution.Persistence;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Repository.Implementation
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
