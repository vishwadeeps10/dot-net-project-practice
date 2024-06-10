using CollegeApp.data;
using CollegeApp.data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class CollegeRepository<T> : ICollegeRepository<T> where T : class
{
    protected readonly CollegeDbContext _dbContext;
    private DbSet<T> _dbSet;

    public CollegeRepository(CollegeDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public virtual async Task<T> CreateAsync(T dbRecord)
    {
        await _dbSet.AddAsync(dbRecord);
        await _dbContext.SaveChangesAsync();
        return dbRecord;
    }

    public virtual async Task<bool> DeleteByIdasync(T dbRecord)
    {
        _dbSet.Remove(dbRecord);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, bool noTracking = false)
    {
        if (noTracking)
        {
            return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
        }
        else
        {
            return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }
    }

    public virtual async Task<T> GetByNameAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.Where(filter).FirstOrDefaultAsync();
    }

    public virtual async Task<T> UpdateAsync(T dbRecord)
    {
        _dbContext.Update(dbRecord);
        await _dbContext.SaveChangesAsync();
        return dbRecord;
    }
}
