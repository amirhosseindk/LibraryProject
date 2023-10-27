using Application.DTO.Book;
using Application.DTO.Category;

namespace Application.UseCases.Category
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryCDto categoryDto);
        Task UpdateCategory(CategoryUDto categoryDto);
        Task DeleteCategory(int categoryId);
        Task<CategoryRDto> GetCategoryDetails(int categoryId);
        Task<IEnumerable<CategoryRDto>> ListCategories();
        Task<IEnumerable<BookRDto>> ListBooksByCategory(int categoryId);
    }
}