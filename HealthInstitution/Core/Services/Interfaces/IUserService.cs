using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Services.Interfaces
{
    public interface IUserService<T> : ICrudService<T> where T : User
    {
        User Authenticate(string username, string password);
        bool AlreadyInUse(string username);
        bool IsEmailUsed(string email);
    }
}
