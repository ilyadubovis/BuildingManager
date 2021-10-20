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
    [Route("api/buildings")]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingService buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            this.buildingService = buildingService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OkObjectResult> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<Building> buildings = await buildingService.GetAllAsync().ToListAsync(cancellationToken);
            return Ok(buildings.Select(b => b.Name));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Building>> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                Building? building = await buildingService.GetAsync(id, cancellationToken);
                if (building == null)
                {
                    return NotFound();
                }
                return Ok(building);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OkObjectResult> Search([FromQuery] BuildingSearchParameters parameters, CancellationToken cancellationToken)
        {
            IEnumerable<Building> buildings = await buildingService.GetAllAsync(parameters).ToListAsync(cancellationToken);
            return Ok(buildings);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Building>> CreateAsync([FromBody] Building building, CancellationToken cancellationToken)
        {
            try
            {
                Building createdBuilding = await buildingService.AddAsync(building, cancellationToken);
                return Ok(building);
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
        public async Task<ActionResult<Building>> UpdateAsync(
            int id, [FromBody] Building building, CancellationToken cancellationToken)
        {
            try
            {
                Building? existingBuilding = await buildingService.GetAsync(id, cancellationToken);
                if (existingBuilding == null)
                {
                    return NotFound();
                }
                Building? updatedBuilding = await buildingService.UpdateAsync(id, building, cancellationToken);
                return Ok(building);
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
        public async Task<ActionResult<Building>> DeleteAsync(
            int id, [FromBody] Building building, CancellationToken cancellationToken)
        {
            try
            {
                Building? existingBuilding = await buildingService.GetAsync(id, cancellationToken);
                if (existingBuilding == null)
                {
                    return NotFound();
                }
                bool deleteResult = await buildingService.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
