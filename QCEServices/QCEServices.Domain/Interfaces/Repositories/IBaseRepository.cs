using System.Linq.Expressions;
using QCEServices.Domain.Interfaces.Entities;

namespace QCEServices.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> Get();
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}