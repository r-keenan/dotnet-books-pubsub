using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Models;

public class BooksDbContext : DbContext
{

    public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
    { }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publisher { get; set; }

}
