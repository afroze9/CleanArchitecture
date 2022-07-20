using AutoMapper;
using CleanArchitecture.Application.Common.Abstractions;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries;

[Authorize(Policy = AuthorizationPolicy.ProjectSearch)]
public class SearchProjectsQuery : IRequest<PaginatedList<Project>>
{
    public string? Name { get; set; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class SearchProjectsQueryHandler : IRequestHandler<SearchProjectsQuery, PaginatedList<Project>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<Project>> Handle(SearchProjectsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Project> query = _context.Projects.AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }

        return await query
            .OrderBy(x => x.CreatedOnUtc)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}