using HealthInstitution.Core.Utility;
using HealthInstitution.Core.Features.EquipmentManagement.Model;

namespace HealthInstitution.Core.Features.EquipmentManagement.Repository
{
    public interface IEntryRepository : ICrudRepository<Entry<Equipment>>
    {
    }
}
