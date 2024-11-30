namespace EStockFlow.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        
        Task SaveChangesAsync();
    }
}