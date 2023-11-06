using Application.DTO.Category;
using Application.UseCases.Category;
using MediatR;

namespace Application.Query.Category
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryRDto>>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<CategoryRDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.ListCategories(cancellationToken);
        }
    }
}