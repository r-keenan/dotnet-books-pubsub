using Books.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
{
    protected readonly BooksDbContext _context;
    public BaseRepository(BooksDbContext context)
    {
        _context = context;
    }

    public async Task<T> Add(T entity)
    {
        var result = await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public void AddBatch(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        _context.SaveChanges();
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Get(id);
        _context.Set<T>().Remove(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Set<T>().AnyAsync(e => e.Id == id);
    }

    public async Task<T> Get(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        if (entity == null)
        {
            throw new KeyNotFoundException($"{typeof(T).Name} with id {id} not found");
        }

        return entity;
    }

    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> Update(T entity)
    {

        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        var ent = await _context.Set<T>().FindAsync(entity.Id);

        if (ent is null)
        {
            throw new KeyNotFoundException($"{typeof(T).Name} with id {entity.Id} not found");
        }

        return ent;
    }
}
