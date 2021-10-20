namespace Service
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IBuildingService
    {
        IAsyncEnumerable<Building> GetAllAsync(BuildingSearchParameters? parameters = default);

        Task<Building?> GetAsync(int buildingId, CancellationToken cancellationToken);

        Task<Building> AddAsync(Building building, CancellationToken cancellationToken);

        Task<Building> UpdateAsync(int id, Building building, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
