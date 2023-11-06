using Application.Commands.User;
using Application.Query.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _sender.Send(new GetUsersQuery());
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var user = await _sender.Send(new GetUserQuery(userId));

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
        {
            int newUserId = await _sender.Send(createUserCommand);
            return CreatedAtAction("GetByUserId", new { userId = newUserId }, createUserCommand);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, UpdateUserCommand updateUserCommand)
        {
            if (userId != updateUserCommand.UserDto.ID)
                return BadRequest();

            await _sender.Send(updateUserCommand);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            await _sender.Send(new DeleteUserCommand(userId));
            return NoContent();
        }
    }
}