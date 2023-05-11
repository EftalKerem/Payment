using System.Linq.Expressions;
using Case.Entity.Domains;
using Case.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Case.Entity.GenericRepository.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly CaseDbContext dbContext;
    public DbSet<T?> Table { get; set; }

    public Repository(CaseDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.Table = dbContext.Set<T>();
    }

    public async Task<T?> AddAsync(T? entity)
    { 
        var result = await Table.AddAsync(entity); 
        return result.Entity; 
    }

    public async Task AddRangeAsync(List<T> entities)
    {
        await Table.AddRangeAsync(entities);
    }

    public IQueryable<T?> TableNoTracking()
    {
        return Table.AsNoTracking();
    }

    public IQueryable<T?> All()
    {
        return this.Table;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Table.FirstOrDefaultAsync(p => p.Id == id);
        if (entity != null)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.IsDeleted = true;
            Table.Update(entity);
            return await SaveChangesAsync();
        }

        return false;
    }

    public async Task<bool> Delete(T? entity)
    {
        if (entity == null) return false;

        entity.UpdatedDate = DateTime.Now;
        entity.IsDeleted = true;
        Table.Update(entity);
        return await SaveChangesAsync();
    }

    public async Task<bool> DeleteMultiple(List<T?> entities)
    {
        if (entities.Any())
        {
            foreach (var entity in entities)
            {
                entity.UpdatedDate = DateTime.Now;
                entity.IsDeleted = true;
                Table.Update(entity);
            }

            return await SaveChangesAsync();
        }

        return false;
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T?, bool>> where)
    {
        return await Table.FirstOrDefaultAsync(where);
    }

    public IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc)
    {
        if (!isDesc)
            return Table.OrderBy(orderBy);

        return Table.OrderByDescending(orderBy);
    }

    public void Update(T? entity)
    {
        if (entity != null)
        {
            entity.UpdatedDate = DateTime.Now;
            Table.Update(entity);
        }
    }

    public async Task<bool> UpdateRange(List<T?> entities)
    {
        if (entities.Any())
        {
            foreach (var entity in entities)
            {
                entity.UpdatedDate = DateTime.Now;
                Table.Update(entity);
            }

            return await SaveChangesAsync();
        }

        return false;
    }

    public IQueryable<T?> Where(Expression<Func<T?, bool>> where)
    {
        return Table.Where(where);
    }

    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // TODO: Log Exception
            return false;
        }
    }
}