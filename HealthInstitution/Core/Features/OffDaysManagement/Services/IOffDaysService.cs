using System;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.OffDaysManagement.Services
{
    public interface IOffDaysService
    {
        public bool IsDoctorInOffice(Doctor doctor, DateTime fromDate, DateTime toDate);
    }
}
