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
            ProductCategoryEnum? category,
            decimal? price,
            decimal? amount,
            int pageNumber,
            int pageSize);
        
        Task<PaginatedList<Transaction>> GetPagedReports(
            string? name,
            decimal? price,
            int? stock,
            ProductCategoryEnum? category,
            int? quantity,
            decimal? amount,
            int pageNumber,
            int pageSize);
    }
}