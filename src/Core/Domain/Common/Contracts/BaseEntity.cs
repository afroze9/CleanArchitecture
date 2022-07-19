using System.Collections.ObjectModel;
using CleanArchitecture.Common.Contracts;

namespace CleanArchitecture.Domain.Common.Contracts;

public abstract class BaseEntity : IEntity
{
    private readonly List<IntegrationEvent> _domainEvents = new ();

    public Guid Id { get; set; }

    public ReadOnlyCollection<IntegrationEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IntegrationEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}