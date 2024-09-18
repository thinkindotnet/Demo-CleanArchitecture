using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using MyApp.Application.Common.Interfaces;
using MyApp.Domain.Common;


namespace MyApp.Infrastructure.Data.Interceptors;

public class AuditableEntitySaveChangesInterceptor
    : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditableEntitySaveChangesInterceptor(
        ICurrentUserService userService)
    {
        _currentUserService = userService;
    }


    public override int SavedChanges(
        SaveChangesCompletedEventData eventData, 
        int result)
    {
        AuditEntities(eventData.Context);

        return base.SavedChanges(eventData, result);
    }


    public override ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData, 
        int result, 
        CancellationToken cancellationToken = default)
    {

        AuditEntities(eventData.Context);

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }


    private void AuditEntities(DbContext? context)
    {
        if(context is null)
        {
            return;
        }


        foreach(var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUserService.UserId;
                entry.Entity.CreatedAtUtc = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified || entry.State == EntityState.Added ) 
            {
                entry.Entity.LastModifiedBy = _currentUserService.UserId;
                entry.Entity.LastModifiedAtUtc = DateTime.UtcNow;
            }

        }
    }
}
