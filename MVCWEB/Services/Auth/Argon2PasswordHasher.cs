using MVCWEB.Services.Abstract;
using Isopoh.Cryptography.Argon2;
namespace MVCWEB.Services.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return Argon2.Hash(password);
        }

        public bool VerifyPassword(string? password, string? HashedPassword)
        {
            //if (password == null || HashedPassword == null)
            //{
            //    return false;
            //}
            var result = Argon2.Verify(HashedPassword, password);
            return result;
        }
    }
}
