using Books.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    protected readonly BooksDbContext _context;

    public BookRepository(BooksDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<Book> GetWithDetails(int id)
    {
        var entity = await _context
            .Books.Include(b => b.Author)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Book with id {id} not found");
        }
        return entity;
    }

    public async Task<List<Book>> GetAllWithDetails()
    {
        var entities = await _context
            .Books.Include(b => b.Author)
            .Include(b => b.Publisher)
            .ToListAsync();

        if (entities == null)
        {
            return [];
        }

        return entities;
    }
}
