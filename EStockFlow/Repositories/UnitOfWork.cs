using EStockFlow.Data;

namespace EStockFlow.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IProductRepository _productRepository;
        private ITransactionRepository _transactionRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }

                return _productRepository;
            }
        }

        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                {
                    _transactionRepository = new TransactionRepository(_context);
                }

                return _transactionRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}