using EStockFlow.Data;
using EStockFlow.Entities;
using EStockFlow.Enums;
using EStockFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace EStockFlow.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PaginatedList<Product>> GetPagedProducts(
            string? name,
            decimal? price,
            int? stock,
            ProductCategoryEnum? category,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Products.AsNoTracking();

            if (name != null)
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            if (price.HasValue)
            {
                query = query.Where(p => p.Price >= price.Value);
            }

            if (stock.HasValue)
            {
                query = query.Where(p => p.Stock >= stock.Value);
            }

            if (category.HasValue)
            {
                query = query.Where(p => p.Category == category.Value);
            }
            
            query = query.OrderByDescending(p => p.CreatedAt);

            return await PaginatedList<Product>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<PaginatedList<Product>> GetPagedStocks(
            string? name,
            decimal? price,
            int? stock,
            ProductCategoryEnum? category,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Products
                .Where(p => p.Stock > 0)
                .AsNoTracking();

            if (name != null)
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            if (price.HasValue)
            {
                query = query.Where(p => p.Price >= price.Value);
            }

            if (stock.HasValue)
            {
                query = query.Where(p => p.Stock >= stock.Value);
            }

            if (category.HasValue)
            {
                query = query.Where(p => p.Category == category.Value);
            }
            
            query = query.OrderByDescending(p => p.CreatedAt);

            return await PaginatedList<Product>.CreateAsync(query, pageNumber, pageSize);
        }

        public bool IsProductExists(Guid id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public bool IsSufficientStock(int quantity, Guid? id)
        {
            var product = _context.Products.Find(id);

            return (product != null) && (product.Stock >= quantity);
        }
    }
}