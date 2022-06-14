using System;

namespace HealthInstitution.Core.Utility.HelperClasses
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        DateTime CreatedAt { get; set; }

        bool IsActive { get; set; }
    }
}
