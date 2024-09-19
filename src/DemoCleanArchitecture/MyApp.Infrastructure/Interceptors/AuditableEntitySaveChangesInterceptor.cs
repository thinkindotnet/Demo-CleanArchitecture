using Microsoft.EntityFrameworkCore.Diagnostics;
using MyApp.Application.Common.Interfaces;
using MyApp.Domain.Common;
using MyApp.Infrastructure.Extensions;


namespace MyApp.Infrastructure.Interceptors;


public class AuditableEntitySaveChangesInterceptor
    : SaveChangesInterceptor
{

    private readonly ICurrentUserService _currentUserService;


    public AuditableEntitySaveChangesInterceptor(
        ICurrentUserService userService)
    {
        _currentUserService = userService;
    }


    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateAuditPropertiesInEntity(eventData.Context);

        return base.SavingChanges(eventData, result);
    }


    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditPropertiesInEntity(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    #region Helper Methods

    private void UpdateAuditPropertiesInEntity(
        DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            var utcNow = DateTime.UtcNow;

            if (entry.State is EntityState.Added or EntityState.Modified
                || entry.HasChangedOwnedEntities())
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.CreatedAtUtc = utcNow;
                }
                entry.Entity.LastModifiedBy = _currentUserService.UserId;
                entry.Entity.LastModifiedAtUtc = utcNow;
            }
        }
    }

    #endregion

}
