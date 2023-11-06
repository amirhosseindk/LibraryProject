﻿using Application.DTO.Category;
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

        public async Task<int> CreateCategory(CategoryCDto categoryDto,CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var category = new Category
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description
                };

                await _categoryRepository.AddAsync(category,cancellationToken);
                _unitOfWork.Commit();

                return category.ID;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task UpdateCategory(CategoryUDto categoryDto, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryDto.ID,cancellationToken);
                if (category == null)
                {
                    throw new Exception("Category not found.");
                }

                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;

                _categoryRepository.UpdateAsync(category,cancellationToken);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task DeleteCategory(int categoryId,CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
                if (category == null)
                {
                    throw new Exception("Category not found.");
                }

                _categoryRepository.DeleteAsync(category,cancellationToken);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<CategoryRDto> GetCategoryDetails(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
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

        public async Task<CategoryRDto> GetCategoryDetails(string name,CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByNameAsync(name, cancellationToken);
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

        public async Task<IEnumerable<CategoryRDto>> ListCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            return categories.Select(c => new CategoryRDto
            {
                ID = c.ID,
                Name = c.Name,
                Description = c.Description
            });
        }
    }
}