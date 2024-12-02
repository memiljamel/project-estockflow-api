using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EStockFlow.Entities;
using EStockFlow.Models;
using EStockFlow.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace EStockFlow.Endpoints
{
    public static class AccountEndpoint
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/account")
                .WithTags("Account");

            group.MapGet("/current", GetAccount)
                .RequireAuthorization();

            group.MapPost("/register", Register)
                .AllowAnonymous();

            group.MapPost("/login", Login)
                .AllowAnonymous();
        }

        private static async Task<Results<Ok<UserResponse>, NotFound>> GetAccount(
            ClaimsPrincipal claimsPrincipal,
            [FromServices] IUnitOfWork unitOfWork)
        {
            var username = claimsPrincipal.Identity?.Name;
            if (username == null)
            {
                return TypedResults.NotFound();
            }

            var user = await unitOfWork.UserRepository.GetByUsername(username);
            if (user == null)
            {
                return TypedResults.NotFound();
            }

            var response = ToUserResponse(user);

            return TypedResults.Ok(response);
        }

        private static async Task<Results<Created<UserResponse>, ValidationProblem>> Register(
            [FromServices] IValidator<RegisterRequest> validator,
            [FromServices] IUnitOfWork unitOfWork,
            [FromBody] RegisterRequest request)
        {
            var result = await validator.ValidateAsync(request);

            if (result.IsValid)
            {
                var user = new User
                {
                    Name = request.Name,
                    Username = request.Username,
                    Password = BC.HashPassword(request.Password),
                };
                unitOfWork.UserRepository.Add(user);

                await unitOfWork.SaveChangesAsync();

                var response = ToUserResponse(user);

                return TypedResults.Created(string.Empty, response);
            }

            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        private static async Task<Results<Created<LoginResponse>, ValidationProblem>> Login(
            [FromServices] IValidator<LoginRequest> validator,
            [FromServices] IUnitOfWork unitOfWork,
            [FromServices] IConfiguration configuration,
            [FromBody] LoginRequest request)
        {
            var result = await validator.ValidateAsync(request);

            if (result.IsValid)
            {
                var user = await unitOfWork.UserRepository.GetByUsername(request.Username);

                if (user == null || !BC.Verify(request.Password, user.Password))
                {
                    result.Errors.Add(new ValidationFailure("username", "Invalid login attempt."));

                    return TypedResults.ValidationProblem(result.ToDictionary());
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                
                var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials
                );

                var handler = new JwtSecurityTokenHandler();

                return TypedResults.Created(string.Empty, new LoginResponse
                {
                    AccessToken = handler.WriteToken(token)
                });
            }

            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        private static UserResponse ToUserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}