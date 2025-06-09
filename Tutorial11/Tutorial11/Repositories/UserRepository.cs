using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tutorial11.Database;
using Tutorial11.Models;
using Tutorial11.Utils;

namespace Tutorial11.Repositories
{
    public class UserRepository(UsersDbContext dbContext) : IUserRepository
    {
        public async Task<User?> FindUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users
                                  .Where(u => u.RefreshToken == refreshToken)
                                  .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> FindUserByLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users
                                  .Where(u => u.Login == login)
                                  .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
        {
            await dbContext.Users.AddAsync(user, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUserRefreshTokenAsync(User user)
        {
            user.RefreshToken = SecUtils.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            await dbContext.SaveChangesAsync();
        }
    }
}
