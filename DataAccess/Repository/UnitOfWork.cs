using Infrastructure.Interface.IRepository;
using Infrastructure.Persistence;

namespace DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    #region Readonlys

    private readonly ApplicationDbContext _db;

    #endregion

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="db">The Database Context</param>
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
        User = new UserRepository(_db);
        Order = new OrderRepository(_db);
    }

    #region Methods

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }

    #endregion

    #region Properties

    public ICategoryRepository Category { get; }
    public IProductRepository Product { get; }
    public IUserRepository User { get; }
    public IOrderRepository Order { get; }

    #endregion
}