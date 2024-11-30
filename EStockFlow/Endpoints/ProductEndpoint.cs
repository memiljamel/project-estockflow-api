using EStockFlow.Entities;
using EStockFlow.Enums;
using EStockFlow.Models;
using EStockFlow.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EStockFlow.Endpoints
{
    public static class ProductEndpoint
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/items")
                .WithTags("Items");

            group.MapPost("/", CreateProduct)
                .DisableAntiforgery();

            group.MapGet("/", GetProducts);

            group.MapPut("/{itemId:guid}", UpdateProduct)
                .DisableAntiforgery();

            group.MapDelete("/{itemId:guid}", DeleteProduct);
        }

        private static async Task<Results<Created<ProductResponse>, ValidationProblem>> CreateProduct(
            [FromServices] IValidator<CreateProductRequest> validator,
            [FromServices] IUnitOfWork unitOfWork,
            [FromForm] CreateProductRequest request)
        {
            var result = await validator.ValidateAsync(request);

            if (result.IsValid)
            {
                var filename = Path.GetRandomFileName() + Path.GetExtension(request.Image.FileName);

                await using var stream = new FileStream(Path.Combine("wwwroot", filename), FileMode.Create);
                await request.Image.CopyToAsync(stream); 

                var product = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    InitialStock = request.InitialStock,
                    Category = request.Category,
                    ImageUrl = filename,
                };
                unitOfWork.ProductRepository.Add(product);

                await unitOfWork.SaveChangesAsync();

                var response = ToProductResponse(product);

                return TypedResults.Created(string.Empty, response);
            }

            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        private static async Task<Results<Ok<PaginatedList<ProductResponse>>, NotFound>> GetProducts(
            [FromServices] IUnitOfWork unitOfWork,
            [FromQuery] string? name,
            [FromQuery] decimal? price,
            [FromQuery] int? initialStock,
            [FromQuery] ProductCategory? category,
            [FromQuery] string? imageUrl,
            [FromQuery(Name = "page")] int pageNumber = 1,
            [FromQuery(Name = "size")] int pageSize = 15)
        {
            if (pageNumber < 1)
            {
                return TypedResults.NotFound();
            }

            var products = await unitOfWork.ProductRepository.GetPagedProducts(
                name,
                price,
                initialStock,
                category,
                imageUrl,
                pageNumber,
                pageSize);

            if (products.PageIndex != 1 && pageNumber > products.Count)
            {
                return TypedResults.NotFound();
            }

            var response = products.Select(p => ToProductResponse(p))
                .ToList();

            return TypedResults.Ok(new PaginatedList<ProductResponse>(
                response,
                products.Count,
                products.PageIndex,
                pageSize));
        }

        private static async Task<Results<Ok<ProductResponse>, NotFound, ValidationProblem>> UpdateProduct(
            [FromServices] IValidator<UpdateProductRequest> validator,
            [FromServices] IUnitOfWork unitOfWork,
            [FromRoute] Guid itemId,
            [FromForm] UpdateProductRequest request)
        {
            request.Id = itemId;

            var result = await validator.ValidateAsync(request);

            if (result.IsValid)
            {
                var product = await unitOfWork.ProductRepository.GetById(itemId);
                if (product == null)
                {
                    return TypedResults.NotFound();
                }

                product.Name = request.Name;
                product.Price = request.Price;
                product.InitialStock = request.InitialStock;
                product.Category = request.Category;
                product.UpdatedAt = DateTime.UtcNow;
                
                if (request.Image != null)
                {
                    if (product.ImageUrl != null && File.Exists(Path.Combine("wwwroot", product.ImageUrl)))
                    {
                        File.Delete(Path.Combine("wwwroot", product.ImageUrl));
                    }
                    
                    var filename = Path.GetRandomFileName() + Path.GetExtension(request.Image.FileName);

                    await using var stream = new FileStream(Path.Combine("wwwroot", filename), FileMode.Create);
                    await request.Image.CopyToAsync(stream);

                    product.ImageUrl = filename;
                }

                await unitOfWork.SaveChangesAsync();

                var response = ToProductResponse(product);

                return TypedResults.Ok(response);
            }

            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        private static async Task<Results<NoContent, NotFound>> DeleteProduct(
            [FromServices] IUnitOfWork unitOfWork,
            [FromRoute] Guid itemId)
        {
            var product = await unitOfWork.ProductRepository.GetById(itemId);
            if (product == null)
            {
                return TypedResults.NotFound();
            }
            
            if (product.ImageUrl != null && File.Exists(Path.Combine("wwwroot", product.ImageUrl)))
            {
                File.Delete(Path.Combine("wwwroot", product.ImageUrl));
            }
            
            unitOfWork.ProductRepository.Remove(product);

            await unitOfWork.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        private static ProductResponse ToProductResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                InitialStock = product.InitialStock,
                Category = product.Category,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}