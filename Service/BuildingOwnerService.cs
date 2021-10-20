using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class BuildingOwnerService : IBuildingOwnerService
    {
        private readonly IBuildingRepo buildingRepo;
        private readonly IBuildingOwnerRepo buildingOwnerRepo;
        public BuildingOwnerService(IBuildingRepo buildingRepo, IBuildingOwnerRepo buildingOwnerRepo)
        {
            this.buildingRepo = buildingRepo;
            this.buildingOwnerRepo = buildingOwnerRepo;
        }

        public IAsyncEnumerable<BuildingOwner> GetAllAsync() =>
            buildingOwnerRepo.GetAllAsync();

        public async Task<BuildingOwner?> GetAsync(int buildingOwnerId, CancellationToken cancellationToken) =>
            await buildingOwnerRepo.GetAsync(buildingOwnerId, cancellationToken);

        public async Task<BuildingOwner> AddAsync(BuildingOwner buildingOwner, CancellationToken cancellationToken) {
            BuildingOwner updatedBuildingOwner = await buildingOwnerRepo.UpsertAsync(buildingOwner, cancellationToken);
            await buildingOwnerRepo.SaveAsync(cancellationToken);
            return updatedBuildingOwner;
        }

        public async Task<BuildingOwner> UpdateAsync(int id, BuildingOwner buildingOwner, CancellationToken cancellationToken)
        {
            BuildingOwner? existingBuildingOwner = await buildingOwnerRepo.GetAsync(id, cancellationToken);
            if (existingBuildingOwner == default)
            {
                throw new InvalidOperationException($"Buiding owner with id {id} does not exist.");
            }
            buildingOwner.Id = id; 
            BuildingOwner updatedBuildingOwner = await buildingOwnerRepo.UpsertAsync(buildingOwner, cancellationToken);
            await buildingOwnerRepo.SaveAsync(cancellationToken);
            return updatedBuildingOwner;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            bool result = await buildingOwnerRepo.DeleteAsync(id, cancellationToken);
            await buildingOwnerRepo.SaveAsync(cancellationToken);
            return result;
        }
    }
}
