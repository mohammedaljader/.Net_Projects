using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Logica_laag_.HashingData
{
    public static class HashData
    {
        public static string PasswordHasher(string password)
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
