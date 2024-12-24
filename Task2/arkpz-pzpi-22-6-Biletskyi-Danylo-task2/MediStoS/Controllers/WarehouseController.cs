namespace MediStoS.Controllers;

using MediStoS.Database.Models;
using MediStoS.Database.Repository.WarehouseRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController(IWarehouseRepository warehouseRepository) : ControllerBase
{
    /// <summary>
    /// Handles a request of getting a warehouse object by its id 
    /// </summary>
    /// <param name="id">Warehouse id<see cref="int"/></param>
    /// <returns>Warehouse object or a message with an error:<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation("Get warehouse object by it's id")]
    [ProducesResponseType(typeof(Warehouse), 200)]
    public async Task<IActionResult> GetWarehouse(int id)
    {
        var warehouse = await warehouseRepository.GetWarehouse(id);
        if (warehouse == null)
        {
            return NotFound($"Warehouse by id {id} was not found");
        }

        return Ok(warehouse);
    }

    /// <summary>
    /// Handles a request of creating a warehouse object on the server.
    /// </summary>
    /// <param name="model">JSON warehouse model that transforms into an object<see cref="WarehouseCreateModel"/></param>
    /// <returns>Successful result or an error: <see cref="Task{IActionResult}"/></returns>
    [HttpPost]
    [Route("")]
    [SwaggerOperation("Create warehouse object")]
    public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseCreateModel model)
    {
        if (model == null)
        {
            return BadRequest($"Warehouse was not added, because data in request body was corrupted");
        }

        Warehouse warehouse = new Warehouse(model);
        bool result = await warehouseRepository.AddWarehouse(warehouse);
        if (!result) return BadRequest("Warehouse was not added successfully");
        return Ok();
    }

    /// <summary>
    /// Handles a request of deleting warehouse object from the server by its id
    /// </summary>
    /// <param name="id">Warehouse id<see cref="int"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpDelete]
    [Route("{id}")]
    [SwaggerOperation("Delete warehouse object by it's id")]
    public async Task<IActionResult> DeleteWarehouse(int id)
    {
        Warehouse? warehouse = await warehouseRepository.GetWarehouse(id);
        if (warehouse == null)
        {
            return NotFound($"Warehouse by id {id} was not found and therefore can't be deleted");
        }

        bool result = await warehouseRepository.DeleteWarehouse(warehouse);
        if (!result) return BadRequest("Warehouse was not deleted successfully");
        return Ok();
    }

    /// <summary>
    /// Handles a request of updating a warehouse by its id and new model
    /// </summary>
    /// <param name="model">Warehouse updated data<see cref="WarehouseCreateModel"/></param>
    /// <param name="id">Warehouse id<see cref="int"/></param>
    /// <returns>Ok with warehouse object, or an error: <see cref="Task{IActionResult}"/></returns>
    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation("Update warehouse object specified by it's id")]
    [ProducesResponseType(typeof(Warehouse), 200)]
    public async Task<IActionResult> UpdateWarehouse([FromBody] WarehouseCreateModel model, int id)
    {
        if (model == null)
        {
            return BadRequest("Coudln't get updated warehouse. Data was corrupted");
        }

        var oldWarehouse = await warehouseRepository.GetWarehouse(id, false);
        if (oldWarehouse == null)
        {
            return NotFound($"Warehouse by the specified Id:{id} doesn't exist");
        }

        Warehouse newWarehouse = new Warehouse(model);
        newWarehouse.Id = id;
        var result = await warehouseRepository.UpdateWarehouse(newWarehouse);
        if (!result) return BadRequest("Warehouse was not updated");
        return Ok(newWarehouse);
    }

    /// <summary>
    /// Handles a request of getting all warehouses
    /// </summary>
    /// <returns>Ok with a list of warehouses (inspite empty or not)<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("")]
    [SwaggerOperation("Get all warehouse objects")]
    [ProducesResponseType(typeof(List<Warehouse>), 200)]
    public async Task<IActionResult> GetWarehouses()
    {
        return Ok(await warehouseRepository.GetWarehouses());
    }
}
