using Application.DTO.Category;
using Application.Patterns;
using Application.UseCases.Category;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<int> CreateCategory(CategoryCDto categoryDto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var category = new Category
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description
                };

                await _categoryRepository.AddAsync(category);
                _unitOfWork.Commit();

                return category.ID;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task UpdateCategory(CategoryUDto categoryDto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryDto.ID);
                if (category == null)
                {
                    throw new Exception("Category not found.");
                }

                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;

                _categoryRepository.Update(category);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task DeleteCategory(int categoryId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    throw new Exception("Category not found.");
                }

                _categoryRepository.Delete(category);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<CategoryRDto> GetCategoryDetails(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found.");
            }

            return new CategoryRDto
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryRDto> GetCategoryDetails(string name)
        {
            var category = await _categoryRepository.GetByNameAsync(name);
            if (category == null)
            {
                throw new Exception("Category not found.");
            }

            return new CategoryRDto
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<IEnumerable<CategoryRDto>> ListCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryRDto
            {
                ID = c.ID,
                Name = c.Name,
                Description = c.Description
            });
        }
    }
}