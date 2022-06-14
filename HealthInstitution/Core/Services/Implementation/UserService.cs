using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System.Linq;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Services.Implementation
{
    public class UserService<T> : CrudService<T>, IUserService<T> where T : User
    {
        public UserService(DatabaseContext context) : base(context)
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
