using Application.DTO.Author;
using MediatR;

namespace Application.Query.Author
{
    public class GetAuthorQuery : IRequest<AuthorRDto>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public GetAuthorQuery(int id = 0, string name = null)
        {
            ID = id;
            Name = name;
        }
    }
}