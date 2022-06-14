using HealthInstitution.Core.Utility;
using HealthInstitution.Core.Features.EquipmentManagement.Model;

namespace HealthInstitution.Core.Features.EquipmentManagement.Repository
{
    internal interface IEntryRepository : ICrudRepository<Entry<Equipment>>
    {
    }
}
