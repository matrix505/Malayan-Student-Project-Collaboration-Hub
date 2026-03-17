
namespace MVCWEB.Services.Abstract

{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password,string HashedPassword);
    }
}
