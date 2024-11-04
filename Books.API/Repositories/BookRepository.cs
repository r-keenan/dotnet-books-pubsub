using Books.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Repositories;

public class BookRepository : IBookRepository
{
    protected readonly BooksDbContext _context;

    public BookRepository(BooksDbContext context)
    {
        _context = context;
    }
    public async Task<Book> Add(Book book)
    {
        var result = await _context.AddAsync(book);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Get(id);
        _context.Books.Remove(entity);
        var result = await _context.SaveChangesAsync();
        // result == the amount of rows affected
        return result > 0;
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Books.AnyAsync(a => a.Id == id);
    }

    public async Task<Book> Get(int id)
    {
        var entity = await _context.Books.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Book with id {id} not found");
        }
        return entity;
    }

    public async Task<List<Book>> GetAll()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book> Update(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        var entity = await _context.Books.FindAsync(book.Id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Book with id {book.Id} not found");
        }
        return entity;
    }
}
