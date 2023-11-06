using Application.DTO.Category;
using Application.UseCases.Category;
using MediatR;

namespace Application.Query.Category
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryRDto>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryRDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            if (request.ID != 0 && request.Name == null)
            {
                return await _categoryService.GetCategoryDetails(request.ID);
            }
            else if (request.Name != null && request.ID == 0)
            {
                return await _categoryService.GetCategoryDetails(request.Name);
            }
            else
            {
                throw new ArgumentException("Invalid query parameters.");
            }
        }
    }
}