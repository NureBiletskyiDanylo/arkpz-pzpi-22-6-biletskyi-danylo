using MediStoS.Database.Models;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediStoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            User? user = await userRepository.GetUser(id);
            if (user == null) return NotFound($"User with specified id : {id} was not found");

            return Ok(user);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User? user = await userRepository.GetUser(id);
            if (user == null) return NotFound($"User with specified id : {id} was not found");

            bool result = await userRepository.DeleteUser(user);
            if (!result) return BadRequest("User was not deleted");
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserCreateModel model, int id)
        {
            if (model == null)
            {
                return BadRequest("Update user model was corrupted");
            }

            User? user = await userRepository.GetUser(id, false);
            if (user == null) return NotFound($"User with specified id : {id} was not found");

            User newUser = new User(model);
            newUser.Id = id;
            bool result = await userRepository.UpdateUser(newUser);
            if (!result) return BadRequest("User was not updated");
            return Ok(newUser);
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            List<User> users = await userRepository.GetUsers();
            return Ok(users);
        }
    }
}
