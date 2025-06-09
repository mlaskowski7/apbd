using Tutorial11.Contracts.Request;

namespace Tutorial11.Services
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterRequestDto registerReq, CancellationToken cancellationToken = default);

        Task<(string accessToken, string refreshToken)?> LoginUserAsync(LoginRequestDto loginReq, CancellationToken cancellationToken = default);

        Task<(string accessToken, string refreshToken)?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    }
}
