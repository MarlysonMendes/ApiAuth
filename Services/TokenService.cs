using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ApiAuth.Models;
using System.Security.Claims;

namespace ApiAuth.Services
{
    public static class TokenService
    {

        public static string GenerateToken(User user)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(Settings.Secret);
            var TokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature),

            };
            var token = TokenHandler.CreateToken(TokenDescription);
            return TokenHandler.WriteToken(token);
        }

    }
}