using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.OffDaysManagement.Model;
using HealthInstitution.Core.Features.OffDaysManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.OffDaysManagement.Service
{
    public class OffDaysService : IOffDaysService
    {
        private readonly IOffDaysRequestRepository _offDaysRequestRepository;

        public OffDaysService(IOffDaysRequestRepository offDaysRequestRepository)
        {
            _offDaysRequestRepository = offDaysRequestRepository;
        }

        public bool IsDoctorInOffice(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return _offDaysRequestRepository.ReadAll().Where(e => e.Doctor == doctor)
                                                      .Where(e => e.Status == Status.Approved)
                                                      .Count(e => e.StartDate <= toDate && fromDate <= e.EndDate) == 0;
        }

        public IEnumerable<OffDaysRequest> GetOffDaysForDoctor(Doctor doctor)
        {
            return _offDaysRequestRepository.ReadAll().Where(offDaysRequest => offDaysRequest.Doctor == doctor);
        }

        public IEnumerable<OffDaysRequest> GetPendingOffDaysRequests()
        {
            return _offDaysRequestRepository.ReadAll().Where(offDaysRequest => offDaysRequest.Status == Status.Pending);
        }

        public IEnumerable<OffDaysRequest> FilterPendingOffDaysRequestsForSearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _offDaysRequestRepository.ReadAll().Where(offDaysRequest =>
                offDaysRequest.Doctor.EmailAddress.ToLower().Contains(searchText) ||
                offDaysRequest.StartDate.ToString().Contains(searchText) ||
                offDaysRequest.EndDate.ToString().Contains(searchText)).ToList();
        }

        public void AcceptRequest(Guid id)
        {
            var request = _offDaysRequestRepository.Read(id);
            request.Status = Status.Approved;
            _offDaysRequestRepository.Update(request);
        }

        public void DeclineRequest(Guid id, string refuseComment)
        {
            var request = _offDaysRequestRepository.Read(id);
            request.Status = Status.Rejected;
            request.RefuseComment = refuseComment;
            _offDaysRequestRepository.Update(request);
        }
    }
}
