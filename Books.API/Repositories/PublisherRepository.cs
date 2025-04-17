using Books.API.Models;

namespace Books.API.Repositories;

public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
{
    protected readonly BooksDbContext _context;

    public PublisherRepository(BooksDbContext context) : base(context)
    {
        _context = context;
    }
}
