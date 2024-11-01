namespace Books.API;

public interface IPublisherRepository
{
    Task<Publisher> Get(int id);
    Task<List<Publisher>> GetAll();
    Task<Publisher> Add(Publisher publisher);
    Task<bool> Delete(int id);
    Task<Publisher> Update(Publisher publisher);
    Task<bool> Exists(int id);
}
