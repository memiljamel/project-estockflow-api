using EStockFlow.Entities;
using EStockFlow.Enums;
using EStockFlow.Models;

namespace EStockFlow.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<PaginatedList<Transaction>> GetPagedTransactions(
            Guid? item,
            int? quantity,
            ProductCategory? category,
            decimal? price,
            decimal? amount,
            int pageNumber,
            int pageSize);
    }
}