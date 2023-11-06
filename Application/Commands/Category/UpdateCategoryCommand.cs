using Application.DTO.Category;
using MediatR;

namespace Application.Commands.Category
{
    public class UpdateCategoryCommand : IRequest<Unit>
    {
        public CategoryUDto CategoryDto { get; set; }

        public UpdateCategoryCommand(CategoryUDto categoryDto)
        {
            CategoryDto = categoryDto;
        }
    }
}