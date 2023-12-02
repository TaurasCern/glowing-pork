using GPES.Domain.DTO;
using GPES.Domain.Services.IServices;
using System.Security.Cryptography;
using System.Text;

namespace GPES.Domain.Services.Auth
{
    public class PasswordService : IPasswordService
    {
        public PasswordDTO Hash(string password)
        {
            using var hmac = new HMACSHA256();

            return new()
            {
                Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                Salt = hmac.Key
            };
        }

        public bool Verify(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
