using Application.Patterns;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class BookWriteRepository : IBookWriteRepository
    {
        private readonly AppDbContext _context;

        public BookWriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Update(Book book, CancellationToken cancellationToken = default)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Delete(Book book , CancellationToken cancellationToken = default)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}