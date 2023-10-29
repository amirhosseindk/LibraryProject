using Application.DTO.User;
using Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.ListUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var user = await _userService.GetUserDetails(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCDto userCDto)
        {
            int newUserId = await _userService.RegisterUser(userCDto);
            return CreatedAtAction("GetByUserId", new { userId = newUserId }, userCDto);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, [FromBody] UserUDto userUDto)
        {
            if (userId != userUDto.ID)
                return BadRequest();
            await _userService.UpdateUser(userUDto);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }
    }
}