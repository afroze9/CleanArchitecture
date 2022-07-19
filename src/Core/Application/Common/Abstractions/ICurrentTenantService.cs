namespace CleanArchitecture.Application.Common.Abstractions;

public interface ICurrentTenantService
{
    Guid CurrentTenantId { get; }
}