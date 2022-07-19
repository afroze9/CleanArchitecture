using CleanArchitecture.Application.Common.Abstractions;
using CleanArchitecture.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands;

public class CreateProjectCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Name);
        await _context.Projects.AddAsync(project, cancellationToken);
        return project.Id;
    }
}