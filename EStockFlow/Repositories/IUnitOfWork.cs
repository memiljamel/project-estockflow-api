namespace EStockFlow.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        IProductRepository ProductRepository { get; }

        ITransactionRepository TransactionRepository { get; }

        Task SaveChangesAsync();
    }
}