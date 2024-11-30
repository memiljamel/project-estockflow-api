using EStockFlow.Data;

namespace EStockFlow.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IProductRepository _productRepository;
        
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