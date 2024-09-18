using MyApp.Domain.Entities;


namespace MyApp.Application.Common.Interfaces;


public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<TEntity> Set<TEntity>() where TEntity: class;

    DbSet<Category> Categories { get; set; }
}
