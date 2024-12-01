using EStockFlow.Entities;
using EStockFlow.Enums;
using EStockFlow.Models;

namespace EStockFlow.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PaginatedList<Product>> GetPagedProducts(
            string? name,
            decimal? price,
            int? stock,
            ProductCategoryEnum? category,
            int pageNumber,
            int pageSize);
        
        Task<PaginatedList<Product>> GetPagedStocks(
            string? name,
            decimal? price,
            int? stock,
            ProductCategoryEnum? category,
            int pageNumber,
            int pageSize);

        bool IsProductExists(Guid id);

        bool IsSufficientStock(int quantity, Guid? id);
    }
}