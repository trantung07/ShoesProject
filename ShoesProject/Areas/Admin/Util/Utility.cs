using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ShoesProject.Areas.Admin.Util
{
    public class Utility
    {
        
        public static string getHashedMD5(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for(int i=0; i< data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public static bool verifyMD5Hash(string input, string hash)
        {
            string hashedInput = getHashedMD5(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if(comparer.Compare(hashedInput, hash) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}