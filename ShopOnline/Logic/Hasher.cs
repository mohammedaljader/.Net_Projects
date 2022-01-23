using System.Security.Cryptography;
using System.Text;

namespace Data
{
    public class Hasher
    {
        public string GetSha256FromString(string password)
        {
            string hex = "";
            foreach (byte x in new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(password)))
            {
                hex += string.Format("{0:x2}", x);
            }
            return hex.ToUpper();
        }
    }
}