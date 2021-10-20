using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class BuildingOwnerRepo : IBuildingOwnerRepo
    {
        private readonly DataContext dataContext;

        public BuildingOwnerRepo(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IAsyncEnumerable<BuildingOwner> GetAllAsync() =>
            dataContext.BuildingOwner.AsAsyncEnumerable();

        public async Task<BuildingOwner?> GetAsync(int id, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await dataContext.BuildingOwner.AsQueryable().Include(o => o.Buildings).SingleOrDefaultAsync(owner => owner.Id == id);
        }

        public async Task<BuildingOwner> UpsertAsync(BuildingOwner buildingOwner, CancellationToken cancellationToken)
        {
            if (buildingOwner == null)
            {
                throw new ArgumentNullException(nameof(buildingOwner));
            }

            if (buildingOwner.Id == default)
            {
                if(await dataContext.BuildingOwner.AsQueryable().SingleOrDefaultAsync(owner => owner.Email == buildingOwner.Email, cancellationToken) != default)
                {
                    throw new InvalidOperationException($"Buiding owner with email {buildingOwner.Email} already exists.");
                }
                await dataContext.BuildingOwner.AddAsync(buildingOwner, cancellationToken);
            }
            else
            {
                dataContext.BuildingOwner.Update(buildingOwner);
            }

            return buildingOwner;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                throw new ArgumentNullException(nameof(id));
            }

            BuildingOwner buildingOwner = await dataContext.BuildingOwner
                .Include(owner => owner.Buildings)
                .AsQueryable()
                .SingleOrDefaultAsync(owner => owner.Id == id);

            if (buildingOwner == default)
            {
                return false;
            }

            if(buildingOwner.Buildings.Any())
            {
                throw new InvalidOperationException("Buiding owner cannot be deleted, because buildings for this owner exist.");
            }

            dataContext.BuildingOwner.Remove(buildingOwner);

            return true;
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return dataContext.SaveChangesAsync(cancellationToken);
        }  
    }
}
