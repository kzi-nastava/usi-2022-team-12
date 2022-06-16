using System;

namespace HealthInstitution.Core.Exceptions
{
    public class DoctorBusyException : Exception
    {
        public DoctorBusyException() { }

        public DoctorBusyException(string message) : base(message) { }
    }
}
