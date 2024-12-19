using MediStoS.Database.Models;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.DataTransferObjects;
using MediStoS.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediStoS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserRepository userRepository, IConfiguration configuration) : ControllerBase
{
    private TokenService tokenService = new TokenService(configuration);

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (model == null)
        {
            return BadRequest("Login model data was corrupted. Can't log in");
        }

        var checkUser = await userRepository.GetUser(model.Email);
        if (checkUser == null)
        {
            return ValidationProblem("User credentials are incorrect");
        }

        if (model.Password == checkUser.Password)
        {
            var jwtToken = tokenService.Create(checkUser);
            return Ok(new { token = jwtToken });
        }
        return ValidationProblem("User credentials are incorrect");
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserCreateModel model)
    {
        if (model == null)
        {
            return BadRequest("Register model data was corrupted. Can't sing up");
        }

        var checkUser = await userRepository.GetUser(model.Email);
        if (checkUser != null)
        {
            return ValidationProblem("This email is alrady taken");
        }

        User newUser = new User(model);
        bool result = await userRepository.AddUser(newUser);
        if (!result) return BadRequest("Can't register a user");
        var jwtToken = tokenService.Create(newUser);
        return Ok(new { token = jwtToken });
    }
}
