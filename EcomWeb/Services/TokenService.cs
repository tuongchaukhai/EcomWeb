using EcomWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcomWeb.Services
{
    public class TokenService
    {
        private readonly AppSetting _appSetting;

        public TokenService(IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _appSetting = optionsMonitor.CurrentValue;
        }
        public string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("FullName", user.FullName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                    new Claim("Id", user.UserId.ToString()),

                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                (secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);

            ////Save the refreshtoken into the DB
            //var refreshTokenEntity = new RefreshToken
            //{
            //    Id = Guid.NewGuid(),
            //    UserId = user.UserId,
            //    JwtId = token.Id,
            //    Token = refreshToken,
            //    IsUsed = false,
            //    IsRevoked = false,
            //    IssuedAt = DateTime.UtcNow,
            //    ExpiredAt = DateTime.UtcNow.AddMinutes(5),
            //};

            //await _context.AddAsync(refreshTokenEntity);
            //_context.SaveChanges();

            //SetJWTCookie(accessToken); //Set token in a cookie

            return accessToken;
        }
    }
}
