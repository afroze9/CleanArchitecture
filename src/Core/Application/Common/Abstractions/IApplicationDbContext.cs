using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Project> Projects { get; }
}