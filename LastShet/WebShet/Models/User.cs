using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace WebShet.Models
{
    public class User : IComparable<User>
    {
        public static List<User> Users = new List<User>();

        public string Email { get; }
        public string UserName { get; }

        public User(string email, string userName)
        {
            Email = email;
            UserName = userName;
        }

        public int CompareTo(User other)
        {
            return this.Email.CompareTo(other?.Email);
        }

        /// <summary>
        /// Generates random string. 
        /// </summary>
        /// <param name="length"> Length of string. </param>
        /// <returns> Random string. </returns>
        public static string Generator(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bit_count = (length * 6);
                var byte_count = ((bit_count + 7) / 8);
                var bytes = new byte[byte_count];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// Info about messages of the user.
        /// </summary>
        public string MessagesInfo
        {
            get
            {
                var received = Message.Messages.Where(x => x.ReceiverId.Equals(this.Email)).Select(x => x.Subject);
                var sended = Message.Messages.Where(x => x.SenderId.Equals(this.Email)).Select(x => x.Subject);

                return $"Received topics: {string.Join(", ", received)} || " +
                       $"Sended topics: {string.Join(", ", sended)}";
            }
        }
    }
}
