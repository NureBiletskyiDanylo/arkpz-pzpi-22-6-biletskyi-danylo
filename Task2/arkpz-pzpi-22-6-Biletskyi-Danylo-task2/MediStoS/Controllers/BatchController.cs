using MediStoS.Database.Models;
using MediStoS.Database.Repository.BatchRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediStoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController(IBatchRepository batchRepository) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation("Get batch object by it's id")]
        [ProducesResponseType(typeof(Batch), 200)]
        public async Task<IActionResult> GetBatch(int id)
        {
            Batch? batch = await batchRepository.GetBatch(id);
            if (batch == null)
            {
                return NotFound($"Batch with the id of {id} was not found");
            }

            return Ok(batch);
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation("Create a batch object")]
        public async Task<IActionResult> CreateBatch([FromBody] BatchCreateModel model)
        {
            if (model == null)
            {
                return BadRequest($"Batch was not added, because data in request body was corrupted");
            }

            if (!await batchRepository.IsUserExists(model.UserId))
            {
                return NotFound("Can't create a batch by user that doesn't exist");
            }
            if (!await batchRepository.IsMedicineExists(model.MedicineId))
            {
                return NotFound("Can't create for medicine that doesn't exist");
            }
            if (!await batchRepository.IsWarehouseExists(model.WareHouseId))
            {
                return NotFound("Can't create a batch in a warehouse that doesn't exist");
            }

            Batch batch = new Batch(model);
            bool result = await batchRepository.AddBatch(batch);
            if (!result) return BadRequest("Batch was not added");
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation("Delete a batch object by it's id")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            var batch = await batchRepository.GetBatch(id);
            if (batch == null)
            {
                return BadRequest($"Batch with id of {id} was not found");
            }

            bool result = await batchRepository.DeleteBatch(batch);
            if (!result) return BadRequest("Batch was not deleted");
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        [SwaggerOperation("Update batch object specified by it's id")]
        [ProducesResponseType(typeof(Batch), 200)]
        public async Task<IActionResult> UpdateBatch([FromBody] BatchCreateModel model, int id)
        {
            if (model == null)
            {
                return BadRequest("Couldn't get updated batch. Data was corrupted");
            }

            var oldBatch = await batchRepository.GetBatch(id, false);
            if (oldBatch == null)
            {
                return BadRequest($"Batch by the specified Id : {id} doesn't exist");
            }

            if (!await batchRepository.IsUserExists(model.UserId))
            {
                return NotFound("Can't update a batch by user that doesn't exist");
            }
            if (!await batchRepository.IsMedicineExists(model.MedicineId))
            {
                return NotFound("Can't update for medicine that doesn't exist");
            }
            if (!await batchRepository.IsWarehouseExists(model.WareHouseId))
            {
                return NotFound("Can't update a batch in a warehouse that doesn't exist");
            }
            Batch newBatch = new Batch(model);
            newBatch.Id = id;
            var result = await batchRepository.UpdateBatch(newBatch);
            if (!result) return BadRequest("Batch was not updated");
            return Ok(newBatch);
        }

        [HttpGet]
        [Route("{medicineId}/Medicine")]
        [SwaggerOperation("Get all batches, made for specified medicine")]
        [ProducesResponseType(typeof(List<Batch>), 200)]
        public async Task<IActionResult> GetBatches(int medicineId)
        {
            return Ok(await batchRepository.GetBatchesByMedicineId(medicineId));
        }
    }
}
