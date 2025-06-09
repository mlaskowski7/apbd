using Tutorial11.Models;

namespace Tutorial11.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

        Task<User?> FindUserByLoginAsync(string login, CancellationToken cancellationToken = default);

        Task AddUserAsync(User user, CancellationToken cancellationToken = default);

        Task UpdateUserRefreshTokenAsync(User user);
    }
}
