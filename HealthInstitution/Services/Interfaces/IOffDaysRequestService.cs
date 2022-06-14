using System;
using System.Collections.Generic;
using HealthInstitution.Model.doctor;
using HealthInstitution.Model.user;

namespace HealthInstitution.Services.Interfaces
{
    public interface IOffDaysRequestService : ICrudService<OffDaysRequest>
    {
        public IEnumerable<OffDaysRequest> GetOffDaysForDoctor(Doctor doctor);

        public IEnumerable<OffDaysRequest> GetPendingOffDaysRequests();

        public IEnumerable<OffDaysRequest> FilterPendingOffDaysRequestsForSearchText(string searchText);

        public void AcceptRequest(Guid id);

        public void DeclineRequest(Guid id, string reason);
    }
}
