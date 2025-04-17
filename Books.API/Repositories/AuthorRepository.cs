using Books.API.Models;

namespace Books.API.Repositories;

public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
{
    protected readonly BooksDbContext _context;

    public AuthorRepository(BooksDbContext context) : base(context)
    {
        _context = context;
    }

}
