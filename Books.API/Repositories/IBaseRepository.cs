namespace Books.API.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> Get(int id);
    Task<List<T>> GetAll();
    Task<T> Add(T entity);
    void AddBatch(IEnumerable<T> entities);
    Task<bool> Delete(int id);
    Task<T> Update(T entity);
    Task<bool> Exists(int id);
}
