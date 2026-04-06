namespace Constructix.Persistence.Interceptors;
public class AuditInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var now = DateTime.UtcNow;
        var entries = context.ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(x => x.CreatedDate).CurrentValue = now;
                    break;

                case EntityState.Modified:
                    entry.Property(x => x.UpdatedDate).CurrentValue = now;
                    break;

                case EntityState.Deleted:
                    // Soft Delete məntiqi
                    entry.State = EntityState.Modified;
                    entry.Property(x => x.UpdatedDate).CurrentValue = now;
                    entry.Property(x => x.IsDeleted).CurrentValue = true;
                    break;
            }
        }
    }
}