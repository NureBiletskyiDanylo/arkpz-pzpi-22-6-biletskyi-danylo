namespace MediStoS.Controllers;

using MediStoS.Database.Models;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    /// <summary>
    /// Hadnles a request of getting user info by its id
    /// </summary>
    /// <param name="id">User id<see cref="int"/></param>
    /// <returns>Ok with user model or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUser(int id)
    {
        User? user = await userRepository.GetUser(id);
        if (user == null) return NotFound($"User with specified id : {id} was not found");

        return Ok(user);
    }

    /// <summary>
    /// Handles a request of deleting user account by its id
    /// </summary>
    /// <param name="id">User id<see cref="int"/></param>
    /// <returns>Ok or an error:<see cref="Task{IActionResult}"/></returns>
    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Admin,DBAdmin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        User? user = await userRepository.GetUser(id);
        if (user == null) return NotFound($"User with specified id : {id} was not found");

        bool result = await userRepository.DeleteUser(user);
        if (!result) return BadRequest("User was not deleted");
        return Ok();
    }

    /// <summary>
    /// Handles a request of updating user account by its id and model
    /// </summary>
    /// <param name="model">New user data<see cref="UserCreateModel"/></param>
    /// <param name="id">User id<see cref="int"/></param>
    /// <returns>Ok with updated user data, or an error<see cref="Task{IActionResult}"/></returns>
    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Admin,DBAdmin")]
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

    /// <summary>
    /// Handles a request of getting a list of all users
    /// </summary>
    /// <returns>Ok with a list of users, inspite if it is empty or not<see cref="Task{IActionResult}"/></returns>
    [HttpGet]
    [Route("users")]
    [Authorize(Roles = "Admin,DBAdmin")]
    public async Task<IActionResult> GetUsers()
    {
        List<User> users = await userRepository.GetUsers();
        return Ok(users);
    }
}
