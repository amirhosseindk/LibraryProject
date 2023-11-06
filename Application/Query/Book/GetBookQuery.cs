using Application.DTO.Book;
using MediatR;

namespace Application.Query.Book
{
    public class GetBookQuery : IRequest<BookRDto>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public GetBookQuery(int id = 0, string name = null)
        {
            ID = id;
            Name = name;
        }
    }
}