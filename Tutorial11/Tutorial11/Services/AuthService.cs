using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Tutorial11.Contracts.Request;
using Tutorial11.Models;
using Tutorial11.Repositories;
using Tutorial11.Utils;

namespace Tutorial11.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
    {
        public async Task RegisterUserAsync(RegisterRequestDto registerReq, CancellationToken cancellationToken = default)
        {
            var hashedPasswordAndSalt = SecUtils.GetHashedPasswordAndSalt(registerReq.Password);

            var user = new User
            {
                Login = registerReq.Login,
                Email = registerReq.Email,
                Password = hashedPasswordAndSalt.Item1,
                Salt = hashedPasswordAndSalt.Item2,
                RefreshToken = SecUtils.GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
            };

            await userRepository.AddUserAsync(user, cancellationToken);
        }

        public async Task<(string accessToken, string refreshToken)?> LoginUserAsync(LoginRequestDto loginReq, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.FindUserByLoginAsync(loginReq.Login, cancellationToken);
            if (user is null)
            {
                return null;
            }

            var passwordHash = user.Password;
            var hashedPass = SecUtils.GetHashedPasswordWithSalt(loginReq.Password, user.Salt);
            if (passwordHash != hashedPass)
            {
                return null;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            await userRepository.UpdateUserRefreshTokenAsync(user);
            return (new JwtSecurityTokenHandler().WriteToken(token), user.RefreshToken);
        }

        public async Task<(string accessToken, string refreshToken)?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.FindUserByRefreshTokenAsync(refreshToken, cancellationToken);
            if (user is null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return null;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            await userRepository.UpdateUserRefreshTokenAsync(user);
            return (new JwtSecurityTokenHandler().WriteToken(token), user.RefreshToken);
        }
    }
}
