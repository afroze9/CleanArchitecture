namespace CleanArchitecture.Domain.Common.Contracts;

public abstract class AuditableEntity : BaseEntity, IAuditableEntity, ISoftDelete, IHasTenant
{
    public string? CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public Guid TenantId { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }
}