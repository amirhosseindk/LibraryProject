using Application.UseCases.Category;
using MediatR;

namespace Application.Commands.Category
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryService.UpdateCategory(request.CategoryDto, cancellationToken);
            return Unit.Value;
        }
    }
}