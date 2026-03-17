using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using QCEServices.Domain.Interfaces.Entities;

namespace QCEServices.Infrastructure.DataAccess.Interceptors;

public sealed class AuditEntitiesInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        // Get database context
        var dbContext = eventData.Context;
        if(dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);
            
        // Get requestor info if not exist use default
        var requestor = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

        // Get all the entries for base entity
        var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>();
        
        foreach (var entry in entries)
        {
            // Get entity from the entry
            var entity = entry.Entity;

            switch (entry.State)
            {
                case EntityState.Added when entity is ICreatableEntity creatableEntity:
                    creatableEntity.CreatedAt = DateTime.UtcNow;
                    creatableEntity.CreatedBy = requestor;
                    break;
                case EntityState.Modified when entity is IEditableEntity editableEntity:
                    editableEntity.ModifiedAt = DateTime.UtcNow;
                    editableEntity.ModifiedBy = requestor;
                    break;
                case EntityState.Deleted when entity is IDeletableEntity deletableEntity:
                    entry.State = EntityState.Modified;
                    deletableEntity.DeletedAt = DateTime.UtcNow;
                    deletableEntity.DeletedBy = requestor;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}