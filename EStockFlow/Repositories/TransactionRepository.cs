using EStockFlow.Data;
using EStockFlow.Entities;
using EStockFlow.Enums;
using EStockFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace EStockFlow.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PaginatedList<Transaction>> GetPagedTransactions(
            Guid? item,
            int? quantity,
            ProductCategory? category,
            decimal? price,
            decimal? amount,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Transactions.AsNoTracking();

            if (item.HasValue)
            {
                query = query.Where(t => t.ProductId == item.Value);
            }

            if (quantity.HasValue)
            {
                query = query.Where(t => t.Quantity >= quantity.Value);
            }

            if (category.HasValue)
            {
                query = query.Where(t => t.Category == category.Value);
            }

            if (price.HasValue)
            {
                query = query.Where(t => t.Price >= price.Value);
            }
            
            if (amount.HasValue)
            {
                query = query.Where(t => t.Amount >= amount.Value);
            }

            return await PaginatedList<Transaction>.CreateAsync(query, pageNumber, pageSize);
        }
    }
}