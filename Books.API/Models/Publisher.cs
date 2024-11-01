using System.ComponentModel.DataAnnotations;

namespace Books.API;

public class Publisher : BaseModel
{
    [Required]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Name must be between 3 and 100 characters")]
    public string Name { get; set; } = "";
    [StringLength(75, MinimumLength = 10,
        ErrorMessage = "Address1 must be between 10 and 75 characters")]
    public string Address1 { get; set; } = "";
    public string Address2 { get; set; } = "";
    [StringLength(75, MinimumLength = 3,
        ErrorMessage = "City must be between 3 and 75 characters")]
    public string City { get; set; } = "";
    public State State { get; set; }
    [StringLength(10, MinimumLength = 5,
        ErrorMessage = "ZipCode must be between 5 and 10 characters")]
    public string ZipCode { get; set; } = "";
    public DateTime DateFounded { get; set; }
}
