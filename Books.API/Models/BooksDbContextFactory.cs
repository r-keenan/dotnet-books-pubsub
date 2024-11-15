using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.FileProviders;

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

            var environment = new MockWebHostEnvironment { EnvironmentName = "Development" };

            return new BooksDbContext(optionsBuilder.Options, environment);
        }
    }

    public class MockWebHostEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "Books.API";
        public IFileProvider ContentRootFileProvider { get; set; }
        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
        public string EnvironmentName { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string WebRootPath { get; set; }
    }
}
