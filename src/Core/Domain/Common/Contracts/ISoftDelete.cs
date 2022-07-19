namespace CleanArchitecture.Domain.Common.Contracts;

public interface ISoftDelete
{
    string? DeletedBy { get; set; }

    DateTime? DeletedOnUtc { get; set; }
}