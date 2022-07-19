using CleanArchitecture.Common.Contracts;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Events;

public class AddToDoItemEvent : IntegrationEvent
{
    public AddToDoItemEvent(Project project, ToDoItem item)
    {
        Project = project;
        Item = item;
    }

    public Project Project { get; }

    public ToDoItem Item { get; }
}