namespace Infrastructure.Repositories;

using System.Linq.Expressions;
using Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenericReposotory<T> where T : class
{
    private readonly DataBaseContext _context;

    private readonly DbSet<T> _db;
    //DbSet просто робить нам звязок між базою даних і тим шо ми хочемо робити, ми могли би всі операції
    //по пошуку робити через контекст,але там тоді потрібно вказувати в якій табличці шукати,
    //відповідно ми робим дб сет і через дженерік параметр т вказуєм до якої таблички ми хочемо звязок і
    //відповідно через нього робимомо операції з пошуку і тд, а сохраняєм через контекст


    public GenericRepository(DataBaseContext context)
    {
        _context = context;
        _db = _context.Set<T>();
    }

    public async Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
    {
        IQueryable<T> query = _db;
        
        if (expression != null)
        {
            query = query.Where(expression);
        }
        
        if (includes != null)
        {
            foreach (var includeProp in includes)
            {
                query = query.Include(includeProp);
            }
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T> Get(Expression<Func<T, bool>>? expression = null, List<string>? includes = null)
    {
        IQueryable<T> query = _db;

        if (includes != null)
        {
            foreach (var includeProp in includes)
            {
                query = query.Include(includeProp);
            }
        }

        return await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public async Task Create(T entity)
    {
        await _db.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task CreateRange(IEnumerable<T> entities)
    {
        await _db.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public void Update(int id, T entity)
    {
        _db.Update(entity);
    }

    public async void Delete(int id)
    {
        var entity = await _db.FindAsync(id);
        _db.Remove(entity);
        await _context.SaveChangesAsync();
    }
}