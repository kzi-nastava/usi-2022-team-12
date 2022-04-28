using HealthInstitution.Model;

namespace HealthInstitution.Services.Intefaces
{
    public interface IUserService<T> : ICrudService<T> where T : User
    {
        User Authenticate(string username, string password);
        bool AlreadyInUse(string username);
        bool IsEmailUsed(string email);
    }
}
