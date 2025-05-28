using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers
{
    public class HashHelper
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256 instance
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute hash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert to hexadecimal
                }
                return builder.ToString();
            }
        }
    }
}
