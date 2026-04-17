using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace EduBrain.Common.RepositoryPattern;

public interface IRepository<Entity> where Entity : class
{
    Task<Entity?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(Entity entity, CancellationToken ct);
    Task Delete(Entity entity);
    Task UpdateAsync(Entity entity, CancellationToken ct);
    IQueryable<Entity> GetAll();
    IQueryable<Entity> FindAll(Expression<Func<Entity, bool>> predicate);
    Task<Entity?> FindAsync(Expression<Func<Entity, bool>> predicate, CancellationToken ct);
    Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate, CancellationToken ct);
    Task<int> CountAsync(Expression<Func<Entity, bool>> predicate, CancellationToken ct);
    Task AddRangeAsync(IEnumerable<Entity> entities, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);

    Task BulkDeleteWhereAsync(Expression<Func<Entity, bool>> predicate, CancellationToken ct); // Bulk Delete 
    Task BulkUpdateWhereAsync(
    Expression<Func<Entity, bool>> predicate,
    Expression<Func<SetPropertyCalls<Entity>, SetPropertyCalls<Entity>>> setProperty,
    CancellationToken ct); // Bulk Update

    IQueryable<Entity> Include<TProperty>(Expression<Func<Entity, TProperty>> navigationProperty);
}
