namespace Infrastructure.Abstractions;

using Domain.Entities.MainEntities;
using Repositories;

public interface IUnitOfWork : IDisposable
{
    public IGenericReposotory<Order> Orders { get; }
    public IGenericReposotory<Product> Products { get; }

    void Save();
}