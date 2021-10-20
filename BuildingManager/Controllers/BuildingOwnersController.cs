namespace BuildingManager.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Service;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/buildingowners")]
    public class BuildingOwnersController : ControllerBase
    {
        private readonly IBuildingOwnerService buildingOwnerService;

        public BuildingOwnersController(IBuildingOwnerService buildingOwnerService)
        {
            this.buildingOwnerService = buildingOwnerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OkObjectResult> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<BuildingOwner> buildingOwners = await buildingOwnerService.GetAllAsync().ToListAsync(cancellationToken);
            return Ok(buildingOwners.Select(b => b.Name));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BuildingOwner>> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                BuildingOwner? buildingOwner = await buildingOwnerService.GetAsync(id, cancellationToken);
                if (buildingOwner == null)
                {
                    return NotFound();
                }
                return Ok(buildingOwner);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BuildingOwner>> CreateAsync([FromBody] BuildingOwner buildingOwner, CancellationToken cancellationToken)
        {
            try
            {
                BuildingOwner createdBuildingOwner = await buildingOwnerService.AddAsync(buildingOwner, cancellationToken);
                return Ok(buildingOwner);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BuildingOwner>> UpdateAsync(
            int id, [FromBody] BuildingOwner buildingOwner, CancellationToken cancellationToken)
        {
            try
            {
                BuildingOwner? existingBuildingOwner = await buildingOwnerService.GetAsync(id, cancellationToken);
                if (existingBuildingOwner == null)
                {
                    return NotFound();
                }
                BuildingOwner? updatedBuildingOwner = await buildingOwnerService.UpdateAsync(id, buildingOwner, cancellationToken);
                return Ok(buildingOwner);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BuildingOwner>> DeleteAsync(
            int id, [FromBody] BuildingOwner buildingOwner, CancellationToken cancellationToken)
        {
            try
            {
                BuildingOwner? existingBuilding = await buildingOwnerService.GetAsync(id, cancellationToken);
                if (existingBuilding == null)
                {
                    return NotFound();
                }
                bool deleteResult = await buildingOwnerService.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
