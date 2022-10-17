namespace Infrastructure.Repositories;

using Abstractions;
using Abstractions.Repositories;
using Domain.Entities.MainEntities;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataBaseContext _ctx;

    private IGenericReposotory<Order> _orders;
    private IGenericReposotory<Product> _products;

    public UnitOfWork(DataBaseContext ctx, GenericRepository<Order> or)
    {
        _ctx = ctx;
    }

    public IGenericReposotory<Order> Orders => _orders ??= new GenericRepository<Order>(_ctx);

    public IGenericReposotory<Product> Products => _products ??= new GenericRepository<Product>(_ctx);
    
    
    public async void Save()
    {
       await _ctx.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _ctx.Dispose();
        GC.SuppressFinalize(this);
    }
}