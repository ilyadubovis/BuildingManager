using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepo buildingRepo;
        
        private readonly IBuildingOwnerRepo buildingOwnerRepo;

        public BuildingService(IBuildingRepo buildingRepo, IBuildingOwnerRepo buildingOwnerRepo)
        {
            this.buildingRepo = buildingRepo;
            this.buildingOwnerRepo = buildingOwnerRepo;
        }

        public IAsyncEnumerable<Building> GetAllAsync(BuildingSearchParameters? parameters = default) =>
            buildingRepo.GetAllAsync(parameters);

        public async Task<Building?> GetAsync(int buildingId, CancellationToken cancellationToken) =>
            await buildingRepo.GetAsync(buildingId, cancellationToken);

        public async Task<Building> AddAsync(Building building, CancellationToken cancellationToken) {
            BuildingOwner? owner = await buildingOwnerRepo.GetAsync(building.BuildingOwnerId, cancellationToken);
            if(owner == default)
            {
                throw new InvalidOperationException($"Buiding owner with id {building.BuildingOwnerId} does not exist.");
            }
            Building updatedBuilding = await buildingRepo.UpsertAsync(building, cancellationToken);
            await buildingRepo.SaveAsync(cancellationToken);
            return updatedBuilding;
        }

        public async Task<Building> UpdateAsync(int id, Building building, CancellationToken cancellationToken)
        {
            Building? existingBuilding = await buildingRepo.GetAsync(id, cancellationToken);
            if(existingBuilding == default)
            {
                throw new InvalidOperationException($"Buiding with id {id} does not exist.");
            }
            building.Id = id;
            Building updatedBuilding = await buildingRepo.UpsertAsync(building, cancellationToken);
            await buildingRepo.SaveAsync(cancellationToken);
            return updatedBuilding;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            bool result = await buildingRepo.DeleteAsync(id, cancellationToken);
            await buildingRepo.SaveAsync(cancellationToken);
            return result;
        }
    }
}
