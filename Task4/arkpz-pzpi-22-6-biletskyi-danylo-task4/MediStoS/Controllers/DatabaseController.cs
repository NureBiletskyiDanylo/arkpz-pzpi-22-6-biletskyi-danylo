using MediStoS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediStoS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController(IConfiguration configuration) : ControllerBase
    {
        private BackupService _backupService = new BackupService(configuration.GetConnectionString("DefaultConnection"));
        [HttpPost]
        [Route("Backup")]
        [Authorize(Roles = "DBAdmin")]
        public async Task<IActionResult> Backup(string path)
        {
            try
            {
                _backupService.CreateBackup(path);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return BadRequest(exc.Message);
            }

            return Ok(path);
        }
    }
}
