namespace MediStoS.Controllers;

using MediStoS.Database.Models;
using MediStoS.Database.Repository.StorageViolationRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class StorageViolationController(IStorageViolationRepository storageViolationRepository) : ControllerBase
{
    /// <summary>
    /// Handles a request of getting a storage violation data by its id
    /// </summary>
    /// <param name="id">Storage violation id<see cref="int"/></param>
    /// <returns>Ok with storage violation, or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{id}")]
    [Authorize]
    [SwaggerOperation("Get storage violation object by it's id")]
    [ProducesResponseType(typeof(StorageViolation), 200)]
    public async Task<IActionResult> GetStorageViolation(int id)
    {
        StorageViolation? storageViolation = await storageViolationRepository.GetStorageViolation(id);
        if (storageViolation == null)
        {
            return NotFound($"Storage Violation by id {id} was not found");
        }

        return Ok(storageViolation);
    }

    /// <summary>
    /// Handles a request of creating a storage violation
    /// </summary>
    /// <param name="model">Storage violation json model<see cref="StorageViolationCreateModel"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpPost]
    [Route("")]
    [Authorize(Roles = "Admin,DBAdmin")]
    [SwaggerOperation("Create storage violation")]
    public async Task<IActionResult> AddStorageViolation([FromBody] StorageViolationCreateModel model)
    {
        if (model == null)
        {
            return BadRequest($"Storage violation was not added, because data in request body was corrupted");
        }

        if (!await storageViolationRepository.IsWarehouseExist(model.WarehouseId))
        {
            return NotFound("Can't create a storage violation in a warehouse that doesn't exist");
        }

        StorageViolation storageViolation = new StorageViolation(model);
        bool result = await storageViolationRepository.AddStorageViolation(storageViolation);
        if (!result) return BadRequest("Storage Violation was not added");
        return Ok();
    }

    /// <summary>
    /// Handles a request of deleting a storage violation by its id
    /// </summary>
    /// <param name="id">Storage violation id<see cref="int"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Admin,DBAdmin")]
    [SwaggerOperation("Delete storage violation object by it's id")]
    public async Task<IActionResult> DeleteStorageViolation(int id)
    {
        StorageViolation? violation = await storageViolationRepository.GetStorageViolation(id);
        if (violation == null)
        {
            return NotFound($"Storage violation by id {id} was not found");
        }

        bool result = await storageViolationRepository.DeleteStorageViolation(violation);
        if (!result) return BadRequest("Storage violation was not deleted");
        return Ok();
    }

    /// <summary>
    /// Handles a request of getting a list of all storage violation in specified by id warehouse
    /// </summary>
    /// <param name="warehouseId">Warehouse id<see cref="int"/></param>
    /// <returns>Ok with a list of storage violations<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{warehouseId}/Violations")]
    [Authorize]
    [SwaggerOperation("Get all storage violations by specified warehouse id")]
    [ProducesResponseType(typeof(List<StorageViolation>), 200)]
    public async Task<IActionResult> GetStorageViolations(int warehouseId)
    {
        return Ok(await storageViolationRepository.GetStorageViolationsByWarehouseId(warehouseId));
    }
}
