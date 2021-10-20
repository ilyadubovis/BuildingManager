using Data;
using Data.Models;
using Microsoft.Extensions.Options;
using Service;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class ServiceUnitTest
    {
        private readonly IBuildingService buildingService;
        private readonly IBuildingOwnerService buildingOwnerService;

        public ServiceUnitTest()
        {
            DataContext dataContext = new DataContext(
                Options.Create<DataSettings>(
                    new DataSettings()
                    {
                        ConnectionString = "<ConnectionString>"
                    }
                )
            );

            buildingService = new BuildingService(new BuildingRepo(dataContext), new BuildingOwnerRepo(dataContext));
            buildingOwnerService = new BuildingOwnerService(new BuildingRepo(dataContext), new BuildingOwnerRepo(dataContext));
        }

        [Fact]
        public async Task BuildingOwnerTest()
        {
            CancellationToken cancellationToken = new CancellationTokenSource().Token;

            int ownerCount = await buildingOwnerService.GetAllAsync().CountAsync(cancellationToken);
            BuildingOwner owner = new BuildingOwner()
            {
                Name = "abc",
                Email = "abc@def.com",
                Phone = "123456789"
            };

            BuildingOwner createdOwner = await buildingOwnerService.AddAsync(owner, cancellationToken);
            int newOwnerCount = await buildingOwnerService.GetAllAsync().CountAsync(cancellationToken);
            Assert.True(newOwnerCount == ownerCount + 1);
            Assert.NotNull(createdOwner);
            Assert.Equal(createdOwner.Email, owner.Email);

            createdOwner.Name = "abc123";
            BuildingOwner updatedOwner = await buildingOwnerService.UpdateAsync(createdOwner.Id, createdOwner, cancellationToken);
            Assert.NotNull(updatedOwner);
            Assert.NotEqual("abc", updatedOwner.Name);

            owner = new BuildingOwner()
            {
                Name = "xyz",
                Email = "abc@def.com",
                Phone = "1111111111"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async() => await buildingOwnerService.AddAsync(owner, cancellationToken));

            bool deleteResult = await buildingOwnerService.DeleteAsync(createdOwner.Id, cancellationToken);
            newOwnerCount = await buildingOwnerService.GetAllAsync().CountAsync(cancellationToken);
            Assert.True(deleteResult);
            Assert.Equal(newOwnerCount, ownerCount);
        }

        [Fact]
        public async Task BuildingTest()
        {
            CancellationToken cancellationToken = new CancellationTokenSource().Token;

            int buildingCount = await buildingService.GetAllAsync().CountAsync(cancellationToken);
            Building building = new Building()
            {
                Name = "Tower 1",
                BuildingOwnerId = 1,
                UnitCount = 10
            };

            Building createdBuilding = await buildingService.AddAsync(building, cancellationToken);
            int newBuildingCount = await buildingService.GetAllAsync().CountAsync(cancellationToken);
            Assert.True(newBuildingCount == buildingCount + 1);
            Assert.NotNull(createdBuilding);
            Assert.Equal(createdBuilding.Name, building.Name);

            createdBuilding.Name = "Palace";
            Building updatedBuilding = await buildingService.UpdateAsync(createdBuilding.Id, createdBuilding, cancellationToken);
            Assert.NotNull(updatedBuilding);
            Assert.NotEqual("Tower 1", updatedBuilding.Name);

            building = await buildingService.GetAsync(updatedBuilding.Id, cancellationToken);
            Assert.Equal("Palace", building.Name);

            bool deleteResult = await buildingService.DeleteAsync(createdBuilding.Id, cancellationToken);
            newBuildingCount = await buildingService.GetAllAsync().CountAsync(cancellationToken);
            Assert.True(deleteResult);
            Assert.Equal(newBuildingCount, buildingCount);
        }
    }
}
