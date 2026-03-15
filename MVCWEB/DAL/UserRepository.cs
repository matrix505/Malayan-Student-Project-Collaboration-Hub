using Dapper;
using MVCWEB.DAL.Abstract;
using MVCWEB.Data;
using MVCWEB.Models;

namespace MVCWEB.DAL
{
    public class UserRepository : IUsersRepository
    {
        public Task UpdatePasswordAsync(int UserId, string HashedPassword)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProfileImage(int UserId, string ImgPath)
        {
            throw new NotImplementedException();
        }
    }
}
