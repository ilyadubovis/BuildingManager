namespace Service
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Data.Models;

    public interface IBuildingOwnerRepo
    {
        Task<BuildingOwner?> GetAsync(int id, CancellationToken cancellationToken);

        IAsyncEnumerable<BuildingOwner> GetAllAsync();

        Task<BuildingOwner> UpsertAsync(BuildingOwner buildingOwner, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
