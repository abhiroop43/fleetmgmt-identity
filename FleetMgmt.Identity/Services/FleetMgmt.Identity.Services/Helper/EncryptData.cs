using System.Security.Cryptography;
using System.Text;

namespace FleetMgmt.Identity.Services.Helper
{
    public class EncryptData
    {
        /// <summary>
        /// Method added to convert password into encypted data 
        /// </summary>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public  string EncryptPassword(string sPassword)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
            // return await Task.Run(() => s.ToString());
        }

        /// <summary>
        /// Below method added but not tested yet for security purpose to store external users password in MARSA Syste=m.
        /// </summary>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public static string Encrypt(string sPassword)
        {
            byte[] saltBytes = { 1, 222, 31, 20, 11, 23, 85, 6 };
            byte[] saltedHashBytes = new HMACMD5(saltBytes).ComputeHash(Encoding.UTF8.GetBytes(sPassword));

            // Preferred way
            // string saltedHashString = Convert.ToBase64String(saltedHashBytes);

            // Your way
            StringBuilder s = new StringBuilder();
            foreach (byte b in saltedHashBytes)
            {
                s.Append(b.ToString("x2").ToUpper());
            }

            return s.ToString();
        }
    }
}