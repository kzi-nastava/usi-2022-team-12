using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IUserService<T> : ICrudService<T> where T : User
    {
        User Authenticate(string username, string password);
        bool AlreadyInUse(string username);
        bool IsEmailUsed(string email);
    }
}
