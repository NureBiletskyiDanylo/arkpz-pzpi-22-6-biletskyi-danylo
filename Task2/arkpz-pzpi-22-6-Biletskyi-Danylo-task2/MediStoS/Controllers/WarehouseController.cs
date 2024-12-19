using MediStoS.Database.Models;
using MediStoS.Database.Repository.WarehouseRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediStoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController(IWarehouseRepository warehouseRepository) : ControllerBase
    {
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

        [HttpPatch]
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

        [HttpGet]
        [Route("")]
        [SwaggerOperation("Get all warehouse objects")]
        [ProducesResponseType(typeof(List<Warehouse>), 200)]
        public async Task<IActionResult> GetWarehouses()
        {
            return Ok(await warehouseRepository.GetWarehouses());
        }
    }
}
