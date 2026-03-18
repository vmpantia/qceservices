using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QCEServices.Domain.Interfaces.Entities;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Infrastructure.DataAccess.Contexts;

namespace QCEServices.Infrastructure.DataAccess.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> Table;

    protected BaseRepository(QCEServicesDbContext context)
    {
        Context = context;
        Table = context.Set<TEntity>();
    }

    public IQueryable<TEntity> Get() => Table;

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate) => Table.Where(predicate);

    public async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) => await Table.SingleOrDefaultAsync(expression, cancellationToken);

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) => await Table.AnyAsync(expression, cancellationToken);

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default) 
    {
        var result = await Table.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
 
    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = Table.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Table.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}