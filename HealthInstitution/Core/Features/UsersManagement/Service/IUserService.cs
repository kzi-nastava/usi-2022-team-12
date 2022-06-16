using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public interface IUserService
    {
        public User Authenticate(string email, string password);

        public bool AlreadyInUse(string email);

        public bool IsEmailUsed(string email);
    }
}
