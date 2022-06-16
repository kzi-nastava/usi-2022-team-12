using System;

namespace HealthInstitution.Core.Exceptions
{
    public class RoomBusyException : Exception
    {
        public RoomBusyException() { }

        public RoomBusyException(string message) : base(message) { }
    }
}
