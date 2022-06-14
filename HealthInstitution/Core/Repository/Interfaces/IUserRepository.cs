using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Repository.Interfaces
{
    public interface IUserRepository<T> : ICrudRepository<T> where T : User
    {
        User Authenticate(string username, string password);

        bool AlreadyInUse(string username);

        bool IsEmailUsed(string email);
    }
}
