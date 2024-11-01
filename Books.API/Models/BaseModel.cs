namespace Books.API;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
