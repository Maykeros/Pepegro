namespace Infrastructure.Abstractions.Repositories;

using System.Linq.Expressions;

public interface IGenericReposotory<T> where T : class
{
    Task<IList<T>> GetAll(
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        List<string>? includes = null);

    Task<T> Get(
        Expression<Func<T, bool>> expression = null,
        List<string>? includes = null);

    Task Create(T entity);
    
    Task CreateRange(IEnumerable<T> entities);

    void Update(int id, T entity);

    void Delete(int id);
}