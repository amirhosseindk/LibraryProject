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
                var res = await _authorService.GetAuthorDetails(request.ID);
                return res;
            }
            else if (request.Name != null && request.ID == 0)
            {
                var res = await _authorService.GetAuthorDetails(request.Name);
                return res;
            }
            else
            {
                throw new Exception("Invalid query");
            }
        }
    }
}