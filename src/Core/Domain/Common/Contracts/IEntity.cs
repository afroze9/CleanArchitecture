using System.Collections.ObjectModel;
using CleanArchitecture.Common.Contracts;

namespace CleanArchitecture.Domain.Common.Contracts;

public interface IEntity
{
    Guid Id { get; set; }

    ReadOnlyCollection<IntegrationEvent> DomainEvents { get; }

    void AddDomainEvent(IntegrationEvent @event);

    void ClearDomainEvents();
}