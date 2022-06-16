using System;

namespace HealthInstitution.Core.Exceptions
{
    public class UpdateFailedException : Exception
    {
        public UpdateFailedException() { }

        public UpdateFailedException(string message) : base(message) { }
    }
}
