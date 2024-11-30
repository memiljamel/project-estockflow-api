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
            int? initialStock,
            ProductCategory? category,
            string? imageUrl,
            int pageNumber,
            int pageSize);

    }
}