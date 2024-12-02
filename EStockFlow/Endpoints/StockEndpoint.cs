using EStockFlow.Entities;
using EStockFlow.Enums;
using EStockFlow.Models;
using EStockFlow.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EStockFlow.Endpoints
{
    public static class StockEndpoint
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/items/stock")
                .WithTags("Stocks");

            group.MapGet("/", GetStocks)
                .RequireAuthorization();
        }

        private static async Task<Results<Ok<PaginatedList<ProductResponse>>, NotFound>> GetStocks(
            [FromServices] IUnitOfWork unitOfWork,
            [FromQuery] string? name,
            [FromQuery] decimal? price,
            [FromQuery] int? stock,
            [FromQuery] ProductCategoryEnum? category,
            [FromQuery(Name = "page")] int pageNumber = 1,
            [FromQuery(Name = "size")] int pageSize = 15)
        {
            if (pageNumber < 1)
            {
                return TypedResults.NotFound();
            }

            var products = await unitOfWork.ProductRepository.GetPagedStocks(
                name,
                price,
                stock,
                category,
                pageNumber,
                pageSize);

            if (products.PageIndex != 1 && pageNumber > products.TotalPages)
            {
                return TypedResults.NotFound();
            }

            var response = products.Select(p => ToProductResponse(p))
                .ToList();

            return TypedResults.Ok(new PaginatedList<ProductResponse>(
                response,
                products.TotalCount,
                products.PageIndex,
                pageSize));
        }

        private static ProductResponse ToProductResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Category = product.Category,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}