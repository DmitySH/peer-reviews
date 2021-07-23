using System;
using System.Security.Cryptography;
using System.Text;

namespace MainApplication.HelpClasses
{
    internal static class MyExtensions
    {
        /// <summary>
        /// Finds hash SHA256 for string.
        /// </summary>
        /// <param name="str"> String. </param>
        /// <returns> SHA256 hash. </returns>
        public static string GetSHA256(this string str)
        {
            byte[] data = Encoding.Default.GetBytes(str);
            byte[] hash = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
