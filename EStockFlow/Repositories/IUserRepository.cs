using EStockFlow.Entities;

namespace EStockFlow.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        bool IsUsernameUnique(string username);

        Task<User?> GetByUsername(string? username);
    }
}