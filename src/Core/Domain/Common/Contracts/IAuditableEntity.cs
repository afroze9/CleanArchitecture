namespace CleanArchitecture.Domain.Common.Contracts;

public interface IAuditableEntity
{
    string CreatedBy { get; set; }

    DateTime CreatedOnUtc { get; set; }

    string? ModifiedBy { get; set; }

    DateTime? ModifiedOnUtc { get; set; }
}