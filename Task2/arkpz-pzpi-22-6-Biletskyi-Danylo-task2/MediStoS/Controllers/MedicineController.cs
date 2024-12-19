using MediStoS.Database.Models;
using MediStoS.Database.Repository.MedicineRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediStoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController(IMedicineRepository medicineRepository) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation("Get medicine object by it's id")]
        [ProducesResponseType(typeof(Medicine), 200)]
        public async Task<IActionResult> GetMedicine(int id)
        {
            var medicine = await medicineRepository.GetMedicine(id);
            if (medicine == null)
            {
                return NotFound($"Medicine with Id={id} was not found");
            }
            return Ok(medicine);
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation("Create a new medicine")]
        public async Task<IActionResult> CreateMedicine([FromBody] MedicineCreateModel model)
        {
            if (model == null)
            {
                return BadRequest($"Medicine was not added, because data in request body was corrupted");
            }

            Medicine medicine = new Medicine(model);
            var result = await medicineRepository.AddMedicine(medicine);
            if (!result) return BadRequest("Medicine was not added");
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation("Delete medicine by it's id")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await medicineRepository.GetMedicine(id);
            if (medicine == null)
            {
                return BadRequest("Selected medicine doesn't exist");
            }

            var result = await medicineRepository.DeleteMedicine(medicine);
            if (!result) return BadRequest("Medicine was not deleted");
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        [SwaggerOperation("Update medcine specified by it's id")]
        [ProducesResponseType(typeof(Medicine), 200)]
        public async Task<IActionResult> UpdateMedicine([FromBody] MedicineCreateModel model, int id)
        {
            if (model == null)
            {
                return BadRequest("Coudln't get updated medicine. Data was corrupted");
            }

            var oldMedicine = await medicineRepository.GetMedicine(id, false);
            if (oldMedicine == null)
            {
                return NotFound($"Medicine by the specified Id:{id} doesn't exist");
            }

            Medicine newMedicine = new Medicine(model);
            newMedicine.Id = id;
            var result = await medicineRepository.UpdateMedicine(newMedicine);
            if (!result) return BadRequest("Medicine was not updated");
            return Ok(newMedicine);
        }

        [HttpGet]
        [Route("")]
        [SwaggerOperation("Get all medicines in a system")]
        [ProducesResponseType(typeof(List<Medicine>), 200)]
        public async Task<IActionResult> GetMedicines()
        {
            return Ok(await medicineRepository.GetMedicines());
        }
    }
}
