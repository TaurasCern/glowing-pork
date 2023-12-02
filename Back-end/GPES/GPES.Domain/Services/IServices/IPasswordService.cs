using GPES.Domain.DTO;

namespace GPES.Domain.Services.IServices
{
    public interface IPasswordService
    {
        PasswordDTO Hash(string password);
        bool Verify(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
