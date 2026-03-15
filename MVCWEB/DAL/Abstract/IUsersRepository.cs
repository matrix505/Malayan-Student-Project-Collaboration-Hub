using MVCWEB.Models;

namespace MVCWEB.DAL.Abstract
{
    public interface IUsersRepository
    {
        Task UpdatePasswordAsync(int UserId, string HashedPassword);
        Task UpdateProfileImage(int UserId, string ImgPath);

    }
}
