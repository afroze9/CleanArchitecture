namespace CleanArchitecture.Domain.Common.Contracts;

public interface IHasTenant
{
    Guid TenantId { get; set; }
}