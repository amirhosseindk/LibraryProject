using Application.DTO.Author;
using Application.Patterns;
using Application.UseCases.Author;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorReadRepository _authorReadRepository;
        private readonly IAuthorWriteRepository _authorWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork, IAuthorReadRepository authorReadRepository, IAuthorWriteRepository authorWriteRepository)
        {
            _unitOfWork = unitOfWork;
            _authorReadRepository = authorReadRepository;
            _authorWriteRepository = authorWriteRepository;
        }

        public async Task<int> CreateAuthor(AuthorCDto authorDto, CancellationToken cancellationToken = default)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingAuthor = await _authorReadRepository.GetByNameAsync(authorDto.LastName,cancellationToken);
                if (existingAuthor != null)
                {
                    throw new Exception("Author with this name already exists.");
                }

                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                };

                await _authorWriteRepository.AddAsync(author, cancellationToken);
                _unitOfWork.Commit();

                return author.ID;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task UpdateAuthor(AuthorUDto authorDto, CancellationToken cancellationToken = default)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingAuthor = await _authorReadRepository.GetByIdAsync(authorDto.ID, cancellationToken);
                if (existingAuthor == null)
                {
                    throw new Exception("Author not found.");
                }

                existingAuthor.FirstName = authorDto.FirstName;
                existingAuthor.LastName = authorDto.LastName;

                await _authorWriteRepository.Update(existingAuthor, cancellationToken);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task DeleteAuthor(int authorId, CancellationToken cancellationToken = default)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingAuthor = await _authorReadRepository.GetByIdAsync(authorId, cancellationToken);
                if (existingAuthor == null)
                {
                    throw new Exception("Author not found.");
                }

                await _authorWriteRepository.Delete(existingAuthor, cancellationToken);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<AuthorRDto> GetAuthorDetails(int authorId, CancellationToken cancellationToken = default)
        {
            var author = await _authorReadRepository.GetByIdAsync(authorId, cancellationToken);
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

        public async Task<AuthorRDto> GetAuthorDetails(string authorName, CancellationToken cancellationToken = default)
        {
            var author = await _authorReadRepository.GetByNameAsync(authorName, cancellationToken);
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

        public async Task<IEnumerable<AuthorRDto>> ListAuthors(CancellationToken cancellationToken = default)
        {
            var authors = await _authorReadRepository.GetAllAsync(cancellationToken);
            return authors.Select(a => new AuthorRDto
            {
                ID = a.ID,
                FirstName = a.FirstName,
                LastName = a.LastName
            });
        }
    }
}