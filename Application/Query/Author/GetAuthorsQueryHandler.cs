using Application.DTO.Author;
using Application.UseCases.Author;
using MediatR;

namespace Application.Query.Author
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorRDto>>
    {
        private readonly IAuthorService _authorService;
        public GetAuthorsQueryHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public Task<IEnumerable<AuthorRDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var res = _authorService.ListAuthors();
            return res;
        }
    }
}