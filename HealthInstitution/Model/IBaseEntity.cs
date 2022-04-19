using System;

namespace HealthInstitution.Model
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        DateTime CreatedAt { get; set; }

        bool IsActive { get; set; }
    }
}
