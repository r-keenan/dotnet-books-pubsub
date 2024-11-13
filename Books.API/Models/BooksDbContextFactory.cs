using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Books.API.Models
{
    public class BooksDbContextFactory : IDesignTimeDbContextFactory<BooksDbContext>
    {
        public BooksDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var connectionString = configuration.GetConnectionString("postgres-books");

            var optionsBuilder = new DbContextOptionsBuilder<BooksDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new BooksDbContext(optionsBuilder.Options);
        }
    }
}
