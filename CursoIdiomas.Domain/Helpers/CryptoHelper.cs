using System.Security.Cryptography;
using System.Text;


namespace CursoIdiomas.Domain.Helpers
{
    public class CryptoHelper
    {
        public static string SHA256Encrypt(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(value);
                var hashBytes = sha256.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
