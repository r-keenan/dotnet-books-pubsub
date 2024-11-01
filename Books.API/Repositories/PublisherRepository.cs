
using Books.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.API;

public class PublisherRepository : IPublisherRepository
{
    protected readonly BooksDbContext _context;

    public PublisherRepository(BooksDbContext context)
    {
        _context = context;
    }
    public async Task<Publisher> Add(Publisher publisher)
    {
        var result = await _context.AddAsync(publisher);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Get(id);
        _context.Publishers.Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Publishers.AnyAsync(a => a.Id == id);
    }

    public async Task<Publisher> Get(int id)
    {
        var entity = await _context.Publishers.FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Publisher with id {id} not found");
        }
        return entity;
    }

    public async Task<List<Publisher>> GetAll()
    {
        return await _context.Publishers.ToListAsync();
    }

    public async Task<Publisher> Update(Publisher publisher)
    {
        _context.Publishers.Update(publisher);
        await _context.SaveChangesAsync();

        var entity = await _context.Publishers.FindAsync(publisher.Id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Publisher with id {publisher.Id} not found");
        }
        return entity;
    }
}
