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
    public class BuildingRepo : IBuildingRepo
    {
        private readonly DataContext dataContext;

        public BuildingRepo(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IAsyncEnumerable<Building> GetAllAsync(BuildingSearchParameters? parameters = default)
        {
            IQueryable<Building> query = dataContext.Building.AsQueryable();
            if(parameters != default)
            {
                if(!string.IsNullOrEmpty(parameters.Name))
                {
                    query = query.Where(b => b.Name == parameters.Name);
                }
                if (parameters.Owner != default)
                {
                    query = query.Where(b => b.BuildingOwnerId == parameters.Owner);
                }
                if (parameters.Count != default)
                {
                    query = query.Where(b => b.UnitCount >= parameters.Count);
                }
            }
            return query.Include(b => b.BuildingOwner).AsAsyncEnumerable();
        }

        public async Task<Building?> GetAsync(int id, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await dataContext.Building.AsQueryable().Include(b => b.BuildingOwner).SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Building> UpsertAsync(Building building, CancellationToken cancellationToken)
        {
            if (building == null)
            {
                throw new ArgumentNullException(nameof(building));
            }

            if (building.Id == default)
            {
                await dataContext.Building.AddAsync(building, cancellationToken);
            }
            else
            {
                dataContext.Building.Update(building);
            }

            return building;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Building building = await dataContext.Building.AsQueryable().SingleOrDefaultAsync(b => b.Id == id);
            if (building == default)
            {
                return false;
            }

            dataContext.Building.Remove(building);

            return true;
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
