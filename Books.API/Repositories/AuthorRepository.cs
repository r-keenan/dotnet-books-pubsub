
using Books.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Repositories;

public class AuthorRepository : IAuthorRepository
{
    protected readonly BooksDbContext _context;

    public AuthorRepository(BooksDbContext context)
    {
        _context = context;
    }
    public async Task<Author> Add(Author author)
    {
        var result = await _context.AddAsync(author);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Get(id);
        _context.Authors.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Authors.AnyAsync(a => a.Id == id);
    }

    public async Task<Author> Get(int id)
    {
        var entity = await _context.Authors.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Author with id {id} not found");
        }
        return entity;
    }

    public async Task<List<Author>> GetAll()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author> Update(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();

        var entity = await _context.Authors.FindAsync(author.Id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Author with id {author.Id} not found");
        }
        return entity;
    }
}
