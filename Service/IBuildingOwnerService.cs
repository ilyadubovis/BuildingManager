namespace Service
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IBuildingOwnerService
    {
        IAsyncEnumerable<BuildingOwner> GetAllAsync();

        Task<BuildingOwner?> GetAsync(int buildingId, CancellationToken cancellationToken);

        Task<BuildingOwner> AddAsync(BuildingOwner buildingOwner, CancellationToken cancellationToken);

        Task<BuildingOwner> UpdateAsync(int id, BuildingOwner buildingOwner, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
