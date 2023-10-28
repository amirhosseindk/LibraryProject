using Application.DTO.Category;

namespace Application.UseCases.Category
{
    public interface ICategoryService
    {
        Task<int> CreateCategory(CategoryCDto categoryDto);
        Task UpdateCategory(CategoryUDto categoryDto);
        Task DeleteCategory(int categoryId);
        Task<CategoryRDto> GetCategoryDetails(int categoryId);
        Task<CategoryRDto> GetCategoryDetails(string name);
        Task<IEnumerable<CategoryRDto>> ListCategories();
    }
}