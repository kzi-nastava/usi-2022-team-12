using System;
using System.Linq;
using HealthInstitution.Core.Features.OffDaysManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.OffDaysManagement.Services
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
    }
}
