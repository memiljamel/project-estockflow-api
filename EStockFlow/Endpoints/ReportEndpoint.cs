using EStockFlow.Entities;
using EStockFlow.Enums;
using EStockFlow.Models;
using EStockFlow.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EStockFlow.Endpoints
{
    public static class ReportEndpoint
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/pos/reports")
                .WithTags("Reports");

            group.MapGet("/", GetReports)
                .RequireAuthorization();
        }

        private static async Task<Results<Ok<ReportResponse>, NotFound>> GetReports(
            [FromServices] IUnitOfWork unitOfWork,
            [FromQuery] string? name,
            [FromQuery] decimal? price,
            [FromQuery] int? stock,
            [FromQuery] ProductCategoryEnum? category,
            [FromQuery] int? quantity,
            [FromQuery] decimal? amount,
            [FromQuery(Name = "page")] int pageNumber = 1,
            [FromQuery(Name = "size")] int pageSize = 15)
        {
            if (pageNumber < 1)
            {
                return TypedResults.NotFound();
            }

            var transactions = await unitOfWork.TransactionRepository.GetPagedReports(
                name,
                price,
                stock,
                category,
                quantity,
                amount,
                pageNumber,
                pageSize);

            if (transactions.PageIndex != 1 && pageNumber > transactions.TotalPages)
            {
                return TypedResults.NotFound();
            }

            var response = transactions.Select(r => ToTransactionResponse(r))
                .ToList();

            return TypedResults.Ok(new ReportResponse
            {
                Total = transactions.TotalCount,
                Transactions = new PaginatedList<TransactionResponse>(
                    response,
                    transactions.TotalCount,
                    transactions.PageIndex,
                    pageSize)
            });
        }

        private static TransactionResponse ToTransactionResponse(Transaction transaction)
        {
            return new TransactionResponse
            {
                Id = transaction.Id,
                Item = new ProductResponse
                {
                    Id = transaction.Product.Id,
                    Name = transaction.Product.Name,
                    Price = transaction.Product.Price,
                    Stock = transaction.Product.Stock,
                    Category = transaction.Product.Category,
                    ImageUrl = transaction.Product.ImageUrl
                },
                Quantity = transaction.Quantity,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt
            };
        }
    }
}