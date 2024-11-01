namespace Books.API;

public interface IAuthorRepository
{
    Task<Author> Get(int id);
    Task<List<Author>> GetAll();
    Task<Author> Add(Author author);
    Task<bool> Delete(int id);
    Task<Author> Update(Author author);
    Task<bool> Exists(int id);
}
