using System.Security.Cryptography;

namespace MVCWEB.Helpers
{
    public class TokenHelper
    {
        public static string GenerateToken() => 
            Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
    }
}
