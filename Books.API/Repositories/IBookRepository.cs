namespace Books.API;

public interface IBookRepository
{
    Task<Book> Get(int id);
    Task<List<Book>> GetAll();
    Task<Book> Add(Book book);
    Task<bool> Delete(int id);
    Task<Book> Update(Book book);
    Task<bool> Exists(int id);
}
