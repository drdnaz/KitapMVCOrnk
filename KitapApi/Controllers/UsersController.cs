using Microsoft.AspNetCore.Mvc;
using KitapApi.Models;
using KitapApi.Services;

namespace KitapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userService.GetUserByUsernameAndPasswordAsync(username, password);
            if (user == null)
                return NotFound();
            return Ok(new { user.Id, user.UserName, user.Role });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(User user)
        {
            var result = await _userService.CreateOrUpdateUserAsync(user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
