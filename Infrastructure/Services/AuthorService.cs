using Application.DTO.Author;
using Application.Patterns;
using Application.UseCases.Author;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateAuthor(AuthorCDto authorDto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingAuthor = await _authorRepository.GetByNameAsync(authorDto.LastName);
                if (existingAuthor != null)
                {
                    throw new Exception("Author with this name already exists.");
                }

                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                };

                await _authorRepository.AddAsync(author);
                _unitOfWork.Commit();

                return author.ID;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task UpdateAuthor(AuthorUDto authorDto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingAuthor = await _authorRepository.GetByIdAsync(authorDto.ID);
                if (existingAuthor == null)
                {
                    throw new Exception("Author not found.");
                }

                existingAuthor.FirstName = authorDto.FirstName;
                existingAuthor.LastName = authorDto.LastName;

                await _authorRepository.Update(existingAuthor);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task DeleteAuthor(int authorId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingAuthor = await _authorRepository.GetByIdAsync(authorId);
                if (existingAuthor == null)
                {
                    throw new Exception("Author not found.");
                }

                await _authorRepository.Delete(existingAuthor);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<AuthorRDto> GetAuthorDetails(int authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
            {
                throw new Exception("Author not found.");
            }

            return new AuthorRDto
            {
                ID = author.ID,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        public async Task<AuthorRDto> GetAuthorDetails(string authorName)
        {
            var author = await _authorRepository.GetByNameAsync(authorName);
            if (author == null)
            {
                throw new Exception("Author not found.");
            }

            return new AuthorRDto
            {
                ID = author.ID,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        public async Task<IEnumerable<AuthorRDto>> ListAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(a => new AuthorRDto
            {
                ID = a.ID,
                FirstName = a.FirstName,
                LastName = a.LastName
            });
        }
    }
}