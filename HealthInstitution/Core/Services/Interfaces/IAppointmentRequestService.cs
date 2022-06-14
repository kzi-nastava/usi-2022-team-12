using System.Collections.Generic;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface IAppointmentRequestService<T> : ICrudService<T> where T : AppointmentRequest
    {
        public IEnumerable<T> ReadAllPendingRequests();

        public IEnumerable<T> FilterPendingRequestsBySearchText(string searchText);
    }
}
