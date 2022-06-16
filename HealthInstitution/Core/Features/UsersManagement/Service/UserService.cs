using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _userRepository;

        public UserService(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string email, string password)
        {
            return _userRepository.Authenticate(email, password);
        }

        public bool AlreadyInUse(string email)
        {
            return _userRepository.AlreadyInUse(email);
        }

        public bool IsEmailUsed(string email)
        {
            return _userRepository.IsEmailUsed(email);
        }
    }
}
