using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions
{
    public class RoomBusyException : Exception
    {
        public RoomBusyException() { }

        public RoomBusyException(string message) : base(message) { }
    }
}
