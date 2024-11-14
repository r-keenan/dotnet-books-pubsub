using Microsoft.EntityFrameworkCore;

namespace Books.API.Models;

public class BooksDbContext : DbContext
{
    public BooksDbContext(DbContextOptions<BooksDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new Exception("DbContext needs to be configured with a connection string.");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Authors
        modelBuilder
            .Entity<Author>()
            .HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "George",
                    LastName = "Orwell",
                    DateOfBirth = new DateOnly(1903, 6, 25),
                    WritingAwards = new string[] { "Prometheus Hall of Fame Award" },
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Author
                {
                    Id = 2,
                    FirstName = "Richard",
                    LastName = "Condon",
                    DateOfBirth = new DateOnly(1915, 3, 18),
                    WritingAwards = Array.Empty<string>(),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Author
                {
                    Id = 3,
                    FirstName = "Fyodor",
                    LastName = "Dostoevsky",
                    DateOfBirth = new DateOnly(1821, 11, 11),
                    WritingAwards = Array.Empty<string>(),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Author
                {
                    Id = 4,
                    FirstName = "Robert",
                    MiddleName = "A.",
                    LastName = "Heinlein",
                    DateOfBirth = new DateOnly(1907, 7, 7),
                    WritingAwards = Array.Empty<string>(),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                }
            );

        // Seed Publishers
        modelBuilder
            .Entity<Publisher>()
            .HasData(
                new Publisher
                {
                    Id = 1,
                    Name = "Penguin Books",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Publisher
                {
                    Id = 2,
                    Name = "McGraw-Hill",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Publisher
                {
                    Id = 3,
                    Name = "The Russian Messenger",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Publisher
                {
                    Id = 4,
                    Name = "G. P. Puntnam's Sons",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                }
            );

        // Seed Books
        modelBuilder
            .Entity<Book>()
            .HasData(
                new Book
                {
                    Id = 1,
                    Title = "1984",
                    AuthorId = 1,
                    PublisherId = 1,
                    DatePublished = new DateOnly(1949, 6, 8),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Book
                {
                    Id = 2,
                    Title = "Animal Farm",
                    AuthorId = 1,
                    PublisherId = 1,
                    DatePublished = new DateOnly(1945, 8, 17),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Book
                {
                    Id = 3,
                    Title = "The Manchurian Candidate",
                    AuthorId = 2,
                    PublisherId = 2,
                    DatePublished = new DateOnly(1959, 4, 27),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Book
                {
                    Id = 4,
                    Title = "Crime and Punishment",
                    AuthorId = 3,
                    PublisherId = 3,
                    DatePublished = new DateOnly(1866, 1, 1),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Book
                {
                    Id = 5,
                    Title = "Demons",
                    AuthorId = 3,
                    PublisherId = 3,
                    DatePublished = new DateOnly(1871, 1, 1),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Book
                {
                    Id = 6,
                    Title = "Stranger in a Strange Land",
                    AuthorId = 4,
                    PublisherId = 4,
                    DatePublished = new DateOnly(1961, 6, 1),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Book
                {
                    Id = 7,
                    Title = "Starship Troopers",
                    AuthorId = 4,
                    PublisherId = 4,
                    DatePublished = new DateOnly(1959, 11, 5),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                },
                new Book
                {
                    Id = 8,
                    Title = "The Moon is a Harsh Mistress",
                    AuthorId = 4,
                    PublisherId = 4,
                    DatePublished = new DateOnly(1966, 6, 2),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                }
            );
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
}
