using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions
{
    public class UpdateFailedException : Exception
    {
        public UpdateFailedException() { }

        public UpdateFailedException(string message) : base(message) { }
    }
}
