namespace Books.Shared;

public abstract class BaseEntity<TId>
{
		public TId Id { get; set; }
}
