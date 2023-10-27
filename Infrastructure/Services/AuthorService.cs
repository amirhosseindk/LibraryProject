using Application.DTO.Author;
using Application.Patterns;
using Application.UseCases.Author;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IRepository<Author> authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAuthor(AuthorCDto authorDto)
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
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAuthor(AuthorUDto authorDto)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(authorDto.ID);
            if (existingAuthor == null)
            {
                throw new Exception("Author not found.");
            }

            existingAuthor.FirstName = authorDto.FirstName;
            existingAuthor.LastName = authorDto.LastName;

            _authorRepository.Update(existingAuthor);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAuthor(int authorId)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(authorId);
            if (existingAuthor == null)
            {
                throw new Exception("Author not found.");
            }

            _authorRepository.Delete(existingAuthor);
            await _unitOfWork.SaveAsync();
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
