namespace Constructix.Domain.Common;
public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; internal set; }
    public DateTime? UpdatedDate { get; internal set; }
    public bool IsDeleted { get; internal set; } = false;
}