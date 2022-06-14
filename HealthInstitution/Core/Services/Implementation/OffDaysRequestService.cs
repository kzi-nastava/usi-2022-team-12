using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.OffDaysManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Services.Implementation
{
    public class OffDaysRequestService : CrudService<OffDaysRequest>, IOffDaysRequestService
    {
        public OffDaysRequestService(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<OffDaysRequest> GetOffDaysForDoctor(Doctor doctor)
        {
            return _entities.Where(offDaysRequest => offDaysRequest.Doctor == doctor);
        }

        public IEnumerable<OffDaysRequest> GetPendingOffDaysRequests()
        {
            return _entities.Where(offDaysRequest => offDaysRequest.Status == Status.Pending);
        }

        public IEnumerable<OffDaysRequest> FilterPendingOffDaysRequestsForSearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(offDaysRequest =>
                offDaysRequest.Doctor.EmailAddress.ToLower().Contains(searchText) ||
                offDaysRequest.StartDate.ToString().Contains(searchText) ||
                offDaysRequest.EndDate.ToString().Contains(searchText)).ToList();
        }

        public void AcceptRequest(Guid id)
        {
            var request = Read(id);
            request.Status = Status.Approved;
            Update(request);
        }

        public void DeclineRequest(Guid id, string refuseComment)
        {
            var request = Read(id);
            request.Status = Status.Rejected;
            request.RefuseComment = refuseComment;
            Update(request);
        }
    }
}
