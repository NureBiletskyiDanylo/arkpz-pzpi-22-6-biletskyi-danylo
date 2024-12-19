using MediStoS.Database.Models;
using MediStoS.Database.Repository.StorageViolationRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediStoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageViolationController(IStorageViolationRepository storageViolationRepository) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
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

        [HttpPost]
        [Route("")]
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

        [HttpDelete]
        [Route("{id}")]
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

        [HttpGet]
        [Route("{warehouseId}/Violations")]
        [SwaggerOperation("Get all storage violations by specified warehouse id")]
        [ProducesResponseType(typeof(List<StorageViolation>), 200)]
        public async Task<IActionResult> GetStorageViolations(int warehouseId)
        {
            return Ok(await storageViolationRepository.GetStorageViolationsByWarehouseId(warehouseId));
        }
    }
}
