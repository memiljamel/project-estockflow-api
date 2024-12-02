using EStockFlow.Data;
using EStockFlow.Entities;
using Microsoft.EntityFrameworkCore;

namespace EStockFlow.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public bool IsUsernameUnique(string username)
        {
            return !_context.Users.Any(u => u.Username == username);
        }

        public async Task<User?> GetByUsername(string? username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}