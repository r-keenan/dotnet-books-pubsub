using System.ComponentModel.DataAnnotations;

namespace Books.API;

public class Book : BaseModel
{
    [Required]
    [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Title must be between 3 and 100 characters")]
    public string Title { get; set; } = "";
    public BookGenre Genre { get; set; }
    [Range(1, 10_000, ErrorMessage = "PageLength must be between 1 and 10,000 characters")]
    public int PageLength { get; set; }
    public DateTime PublicationDate { get; set; }
    public int AuthorId { get; set; }
    [Required]
    public Author Author { get; set; }
    public Publisher PublisherId { get; set; }
    [Required]
    public Publisher Publisher { get; set; }
}
