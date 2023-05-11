using System.Linq.Expressions;
using Case.Entity.Domains;
using Microsoft.EntityFrameworkCore;

namespace Case.Entity.GenericRepository.Repositories;

public interface IRepository<T> where T : BaseEntity 
{
    DbSet<T?> Table { get; }
    Task<T?> AddAsync(T? entity);
    Task AddRangeAsync(List<T> entities);
    IQueryable<T?> TableNoTracking();
    IQueryable<T?> All();
    Task<bool> Delete(T? entity);
    Task<bool> Delete(int id);
    Task<bool> DeleteMultiple(List<T?> entity);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T?, bool>> @where);
    IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc); 
    void Update(T? entity); 
    Task<bool> UpdateRange(List<T?> entities);
    IQueryable<T?> Where(Expression<Func<T?, bool>> where); 
    Task<bool> SaveChangesAsync();
}