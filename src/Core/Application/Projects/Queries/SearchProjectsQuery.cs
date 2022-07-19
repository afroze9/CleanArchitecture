using CleanArchitecture.Application.Common.Abstractions;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Projects.Queries;

public class SearchProjectsQuery : IRequest<List<Project>>
{
    public string? Name { get; set; }
}

public class SearchProjectsQueryHandler : IRequestHandler<SearchProjectsQuery, List<Project>>
{
    private readonly IApplicationDbContext _context;

    public SearchProjectsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> Handle(SearchProjectsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Projects.AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }

        return await query.ToListAsync(cancellationToken);
    }
}