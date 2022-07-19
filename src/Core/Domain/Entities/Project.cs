using CleanArchitecture.Domain.Common.Contracts;
using CleanArchitecture.Domain.Events;

namespace CleanArchitecture.Domain.Entities;

public class Project : AuditableEntity
{
    private readonly List<ToDoItem> _items = new ();

    public Project(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public IEnumerable<ToDoItem> Items => _items.AsReadOnly();

    public void AddItem(ToDoItem item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));
        _items.Add(item);
        AddDomainEvent(new AddToDoItemEvent(this, item));
    }
}