namespace MediStoS.Controllers;

using MediStoS.Database.Models;
using MediStoS.Database.Repository.BatchRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class BatchController(IBatchRepository batchRepository) : ControllerBase
{
    /// <summary>
    /// Handles a request of getting a batch by its id 
    /// </summary>
    /// <param name="id">Batch id<see cref="int"/></param>
    /// <returns>Ok with a batch object, or an error<see cref="Task{IActionResult}"/></returns>
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

    /// <summary>
    /// Handles a request of creating a new batch
    /// </summary>
    /// <param name="model">Batch model<see cref="BatchCreateModel"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
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
        if (!await batchRepository.IsWarehouseExists(model.WarehouseId))
        {
            return NotFound("Can't create a batch in a warehouse that doesn't exist");
        }

        Batch batch = new Batch(model);
        bool result = await batchRepository.AddBatch(batch);
        if (!result) return BadRequest("Batch was not added");
        return Ok();
    }

    /// <summary>
    /// Handles a request of deleting a batch by its id
    /// </summary>
    /// <param name="id">Batch id<see cref="int"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
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

    /// <summary>
    /// Handles a request of updating batch by its id with new model
    /// </summary>
    /// <param name="model">Batch new model<see cref="BatchCreateModel"/></param>
    /// <param name="id">Batch id<see cref="int"/></param>
    /// <returns>Ok with an updated model, or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpPut]
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
        if (!await batchRepository.IsWarehouseExists(model.WarehouseId))
        {
            return NotFound("Can't update a batch in a warehouse that doesn't exist");
        }
        Batch newBatch = new Batch(model);
        newBatch.Id = id;
        var result = await batchRepository.UpdateBatch(newBatch);
        if (!result) return BadRequest("Batch was not updated");
        return Ok(newBatch);
    }

    /// <summary>
    /// Handles a request of getting a list of batches for specific medicine by medicine id
    /// </summary>
    /// <param name="medicineId">Medicine id<see cref="int"/></param>
    /// <returns>Ok with a list of batches<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{medicineId}/Medicine")]
    [SwaggerOperation("Get all batches, made for specified medicine")]
    [ProducesResponseType(typeof(List<Batch>), 200)]
    public async Task<IActionResult> GetBatches(int medicineId)
    {
        return Ok(await batchRepository.GetBatchesByMedicineId(medicineId));
    }
}
