using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebShet.Models;

namespace WebShet.Controllers
{
    public class UserController : Controller
    {
        public static readonly Random rnd = new Random();

        /// <summary>
        /// Creates random users.
        /// </summary>
        /// <param name="numberUsers"> Number of users. </param>
        /// <param name="numberMessages"> NUmber of messages. </param>
        /// <returns> List of users. </returns>
        [HttpPost("create-user-list")]
        public IActionResult CreateUserList([FromQuery] int numberUsers, [FromQuery] int numberMessages)
        {
            try
            {
                for (int i = 0; i < numberUsers; i++)
                {
                    var splitted = System.IO.File.ReadAllText(@"Data/names.txt")
                        .Split(',', StringSplitOptions.RemoveEmptyEntries);
                    string mail = Models.User.Generator(rnd.Next(3, 11)) + "@mail.ru";

                    if (Models.User.Users.Any(x => x.Email.Equals(mail)))
                    {
                        numberUsers++;
                        continue;
                    }

                    var user = new User(mail, splitted[rnd.Next(0, splitted.Length - 1)]);

                    Models.User.Users.Add(user);
                }

                for (int i = 0; i < numberMessages; i++)
                {
                    var sender = Models.User.Users[rnd.Next(0, Models.User.Users.Count)];
                    var receiver = Models.User.Users[rnd.Next(0, Models.User.Users.Count)];

                    var message = new Message(Models.User.Generator(rnd.Next(3, 15)), sender.Email, receiver.Email,
                        Models.User.Generator(rnd.Next(5, 150)));

                    Message.Messages.Add(message);
                }
            }
            catch (Exception)
            {

            }

            Program.SaveData(@"Data/users.json", @"Data/messages.json");
            return Ok(Models.User.Users);
        }

        /// <summary>
        /// Finds user with email.
        /// </summary>
        /// <param name="id"> Email to find. </param>
        /// <returns> User. </returns>
        [HttpGet("get-user-by-id")]
        public IActionResult GetUserById([FromQuery] string id)
        {
            var result = Models.User.Users.FirstOrDefault(x => x.Email.Equals(id));
            if (result == null)
            {
                return NotFound(new { Message = $"Пользователь с Email = {id} не найден" });
            }
            return Ok(result);
        }

        /// <summary>
        /// Gets users with limit and offset.
        /// </summary>
        /// <param name="limit"> Limit. </param>
        /// <param name="offset"> Offset. </param>
        /// <returns> User list. </returns>
        [HttpGet("get-users")]
        public IActionResult GetUsers([FromQuery] int limit, [FromQuery] int offset)
        {
            if (limit <= 0 || offset < 0)
            {
                return BadRequest("Отрицательные значения вводить не стоит");
            }

            return Ok(Models.User.Users.Skip(offset).Take(limit));
        }

        /// <summary>
        /// Creates user.
        /// </summary>
        /// <param name="email"> User's email. </param>
        /// <param name="name"> User's name. </param>
        /// <returns></returns>
        [HttpPost("create-user")]
        public IActionResult CreateUser([FromQuery] string email, [FromQuery] string name)
        {
            if (Models.User.Users.Any(x => x.Email.Equals(email)))
            {
                return BadRequest("Пользователь с Email уже зарегистрирован");
            }

            var user = new User(email, name);

            Models.User.Users.Add(user);
            Program.SaveData(@"Data/users.json", @"Data/messages.json");

            return Ok(user);
        }
    }
}
