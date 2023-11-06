using Application.DTO.Category;

namespace Application.UseCases.Category
{
    public interface ICategoryService
    {
        Task<int> CreateCategory(CategoryCDto categoryDto, CancellationToken cancellationToken = default);
        Task UpdateCategory(CategoryUDto categoryDto, CancellationToken cancellationToken = default);
        Task DeleteCategory(int categoryId, CancellationToken cancellationToken = default);
        Task<CategoryRDto> GetCategoryDetails(int categoryId, CancellationToken cancellationToken = default);
        Task<CategoryRDto> GetCategoryDetails(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryRDto>> ListCategories(CancellationToken cancellationToken = default);
    }
}