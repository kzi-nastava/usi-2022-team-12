using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.OffDaysManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.OffDaysManagement.Service
{
    public interface IOffDaysService
    {
        void AcceptRequest(Guid id);
        void DeclineRequest(Guid id, string refuseComment);
        IEnumerable<OffDaysRequest> FilterPendingOffDaysRequestsForSearchText(string searchText);
        IEnumerable<OffDaysRequest> GetOffDaysForDoctor(Doctor doctor);
        IEnumerable<OffDaysRequest> GetPendingOffDaysRequests();
        public bool IsDoctorInOffice(Doctor doctor, DateTime fromDate, DateTime toDate);
    }
}
