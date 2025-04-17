namespace Books.API.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<Book> GetWithDetails(int id);
    Task<List<Book>> GetAllWithDetails();
}
