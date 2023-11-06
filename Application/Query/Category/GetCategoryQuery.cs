using Application.DTO.Category;
using MediatR;

namespace Application.Query.Category
{
    public class GetCategoryQuery : IRequest<CategoryRDto>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public GetCategoryQuery(int id = 0, string name = null)
        {
            ID = id;
            Name = name;
        }
    }
}