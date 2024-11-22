namespace Books.Common.Messages;

public class AuthorMessage
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string[] WritingAwards { get; set; } = [];
}