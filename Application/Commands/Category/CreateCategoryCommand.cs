using Application.DTO.Category;
using MediatR;

namespace Application.Commands.Category
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public CategoryCDto CategoryDto { get; set; }

        public CreateCategoryCommand(CategoryCDto categoryDto)
        {
            CategoryDto = categoryDto;
        }
    }
}
