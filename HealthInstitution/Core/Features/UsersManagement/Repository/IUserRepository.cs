using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public interface IUserRepository<T> : ICrudRepository<T> where T : User
    {
        User Authenticate(string username, string password);

        bool AlreadyInUse(string username);

        bool IsEmailUsed(string email);
    }
}
