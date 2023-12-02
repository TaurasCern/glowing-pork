using GPES.Domain.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GPES.Domain.Services.Auth
{
    public class JWTService : IJWTService
    {
        private readonly string _secretKey;
        public JWTService(IConfiguration conf)
        {
            // TODO: Get secret key from yaml in some way
            _secretKey = conf.GetValue<string>("Secret");
        }

        public string CreateToken(Guid userId, string[] roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var claimList = new List<Claim>() { new Claim(ClaimTypes.Name, userId.ToString()) };
            claimList.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
