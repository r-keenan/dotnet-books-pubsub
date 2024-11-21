using MediatR;

namespace Books.Common;

public interface IDomainEvent : INotification
{
    DateTime ActionDate { get; }
}
