using System.ComponentModel.DataAnnotations;

namespace Books.API;

public class Book : BaseModel
{
    public string Title { get; set; }
    public BookGenre Genre { get; set; }
    public string PageLength { get; set; }
    public DateTime PublicationDate { get; set; }
    public DateTime DateCreated { get; set; }

    public int AuthorId { get; set; }
    [Required]
    public Author Author { get; set; }
    public Publisher PublisherId { get; set; }
    [Required]
    public Publisher Publisher { get; set; }
}
