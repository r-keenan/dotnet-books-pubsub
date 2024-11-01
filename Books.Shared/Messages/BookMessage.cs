namespace Books.Shared.Messages;

public class BookMessage
{
		public Guid Id { get; set; }
		public Guid BookId { get; set; }
		public Guid AuthorId { get; set; }
		public string BookTitle { get; set; } = "";
		public int PageLength { get; set; }
		public string Genre { get; set; }
		public DateTime DateAdded { get; set; }
}
