namespace Service
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Data.Models;

    public interface IBuildingRepo
    {
        Task<Building?> GetAsync(int id, CancellationToken cancellationToken);

        IAsyncEnumerable<Building> GetAllAsync(BuildingSearchParameters? parameters = default);

        Task<Building> UpsertAsync(Building building, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
