using Application.DTO.Category;
using MediatR;

namespace Application.Query.Category
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryRDto>>
    {
    }
}