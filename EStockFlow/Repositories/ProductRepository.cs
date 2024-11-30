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
            int? initialStock,
            ProductCategory? category,
            string? imageUrl,
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

            if (initialStock.HasValue)
            {
                query = query.Where(p => p.InitialStock >= initialStock.Value);
            }

            if (category.HasValue)
            {
                query = query.Where(p => p.Category == category.Value);
            }

            if (imageUrl != null)
            {
                query = query.Where(p => p.ImageUrl.ToLower().Contains(imageUrl.ToLower()));
            }

            return await PaginatedList<Product>.CreateAsync(query, pageNumber, pageSize);
        }

        public bool IsProductExists(Guid id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public bool IsSufficientStock(int quantity, Guid? id)
        {
            var product = _context.Products.Find(id);

            return (product != null) && (product.InitialStock >= quantity);
        }
    }
}