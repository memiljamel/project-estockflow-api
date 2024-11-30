namespace EStockFlow.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        
        ITransactionRepository TransactionRepository { get; }
        
        Task SaveChangesAsync();
    }
}