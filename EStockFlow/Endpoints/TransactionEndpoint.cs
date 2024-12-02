using EStockFlow.Entities;
using EStockFlow.Enums;
using EStockFlow.Models;
using EStockFlow.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EStockFlow.Endpoints
{
    public static class TransactionEndpoint
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/pos/transactions")
                .WithTags("Transactions");

            group.MapGet("/", GetTransactions)
                .RequireAuthorization();

            group.MapPost("/", CreateTransaction)
                .RequireAuthorization();
        }

        private static async Task<Results<Ok<PaginatedList<TransactionResponse>>, NotFound>> GetTransactions(
            [FromServices] IUnitOfWork unitOfWork,
            [FromQuery] Guid? item,
            [FromQuery] int? quantity,
            [FromQuery] ProductCategoryEnum? category,
            [FromQuery] decimal? price,
            [FromQuery] decimal? amount,
            [FromQuery(Name = "page")] int pageNumber = 1,
            [FromQuery(Name = "size")] int pageSize = 15)
        {
            if (pageNumber < 1)
            {
                return TypedResults.NotFound();
            }

            var transactions = await unitOfWork.TransactionRepository.GetPagedTransactions(
                item,
                quantity,
                category,
                price,
                amount,
                pageNumber,
                pageSize);

            if (transactions.PageIndex != 1 && pageNumber > transactions.TotalPages)
            {
                return TypedResults.NotFound();
            }

            var response = transactions.Select(t => ToTransactionResponse(t))
                .ToList();

            return TypedResults.Ok(new PaginatedList<TransactionResponse>(
                response,
                transactions.TotalCount,
                transactions.PageIndex,
                pageSize));
        }

        private static async Task<Results<Created<TransactionResponse>, ValidationProblem>> CreateTransaction(
            [FromServices] IValidator<CreateTransactionRequest> validator,
            [FromServices] IUnitOfWork unitOfWork,
            [FromBody] CreateTransactionRequest request)
        {
            var result = await validator.ValidateAsync(request);

            if (result.IsValid)
            {
                var transaction = new Transaction
                {
                    ProductId = request.Item,
                    Quantity = request.Quantity,
                    Category = request.Category,
                    Price = request.Price,
                    Amount = (request.Quantity * request.Price)
                };
                unitOfWork.TransactionRepository.Add(transaction);

                transaction.Product.Stock -= request.Quantity;

                await unitOfWork.SaveChangesAsync();

                var response = ToTransactionResponse(transaction);

                return TypedResults.Created(string.Empty, response);
            }

            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        private static TransactionResponse ToTransactionResponse(Transaction transaction)
        {
            return new TransactionResponse
            {
                Id = transaction.Id,
                Item = transaction.ProductId,
                Quantity = transaction.Quantity,
                Category = transaction.Category,
                Price = transaction.Price,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt
            };
        }
    }
}