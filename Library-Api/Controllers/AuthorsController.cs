using Application.Commands.Author;
using Application.Query.Author;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{
		private readonly ISender _sender;

		public AuthorsController(ISender sender)
		{
			_sender = sender;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var authors = await _sender.Send(new GetAuthorsQuery(), cancellationToken);

			if (authors == null)
			{
				return NotFound();
			}

			return Ok(authors);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id,CancellationToken cancellationToken)
		{
			var authors = await _sender.Send(new GetAuthorQuery(id), cancellationToken);

			if (authors == null)
			{
				return NotFound();
			}

			return Ok(authors);
		}

		[HttpGet("name/{name}")]
		public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
		{
			var authors = await _sender.Send(new GetAuthorQuery(0, name), cancellationToken);

			if (authors == null)
			{
				return NotFound();
			}

			return Ok(authors);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateAuthorCommand request, CancellationToken cancellationToken)
		{
			int newAuthorId = await _sender.Send(request, cancellationToken);
			return CreatedAtAction(nameof(GetById), new { id = newAuthorId }, request);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, UpdateAuthorCommand request, CancellationToken cancellationToken)
		{
			if (id != request.AuthorDto.ID)
				return BadRequest();

			await _sender.Send(request, cancellationToken);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			await _sender.Send(new DeleteAuthorCommand(id), cancellationToken);
			return NoContent();
		}
	}
}