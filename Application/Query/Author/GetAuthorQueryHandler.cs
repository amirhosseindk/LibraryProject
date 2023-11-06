using Application.DTO.Author;
using Application.UseCases.Author;
using MediatR;

namespace Application.Query.Author
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, AuthorRDto>
    {
        private readonly IAuthorService _authorService;

        public GetAuthorQueryHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<AuthorRDto> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            if (request.ID != 0 && request.Name == null)
            {
                return await _authorService.GetAuthorDetails(request.ID, cancellationToken);
            }
            else if (request.Name != null && request.ID == 0)
            {
                return await _authorService.GetAuthorDetails(request.Name, cancellationToken);
            }
            else
            {
                throw new Exception("Invalid query");
            }
        }
    }
}