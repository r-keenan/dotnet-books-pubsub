using MediatR;

namespace Books.Shared;

public interface IDomainEvent : INotification
{
	DateTime ActionDate { get; }
}
