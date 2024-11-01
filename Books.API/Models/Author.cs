using System.ComponentModel.DataAnnotations;

namespace Books.API;

public class Author : BaseModel
{
    [Required]
    [StringLength(75, MinimumLength = 3,
        ErrorMessage = "FirstName must be between 3 and 75 characters")]
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    [Required]
    [StringLength(75, MinimumLength = 3,
        ErrorMessage = "LastName must be between 3 and 75 characters")]
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string[] WritingAwards { get; set; } = [];
}
