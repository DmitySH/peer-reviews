using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebShet.Models;

namespace WebShet.Controllers
{
    public class MessageController : Controller
    {
        /// <summary>
        /// Gets messages using ids.
        /// </summary>
        /// <param name="sender"> Sender of message. </param>
        /// <param name="receiver"> Receiver of message. </param>
        /// <returns> List of messages. </returns>
        [HttpGet("get-messages-by-users")]
        public IActionResult GetMessagesByUsers([FromQuery] string sender, [FromQuery] string receiver)
        {
            var res = Message.Messages.Where(x => x.SenderId.Equals(sender) && x.ReceiverId.Equals(receiver));
            if (!res.Any())
            {
                return NotFound(new { Message = $"Такое сообщение не найдено" });
            }
            return Ok(res);
        }

        /// <summary>
        /// Gets messages of receiver. 
        /// </summary>
        /// <param name="receiver"> Email of receiver. </param>
        /// <returns> List of messages. </returns>
        [HttpGet("get-messages-by-receiver")]
        public IActionResult GetMessagesByReceiver([FromQuery] string receiver)
        {
            var res = Message.Messages.Where(x => x.ReceiverId.Equals(receiver));
            if (!res.Any())
            {
                return NotFound(new { Message = $"Такое сообщение не найдено" });
            }
            return Ok(res);
        }

        /// <summary>
        /// Gets messages of sender. 
        /// </summary>
        /// <param name="sender"> Email of sender. </param>
        /// <returns> List of messages. </returns>
        [HttpGet("get-messages-by-sender")]
        public IActionResult GetMessagesBySender([FromQuery] string sender)
        {
            var res = Message.Messages.Where(x => x.ReceiverId.Equals(sender));
            if (!res.Any())
            {
                return NotFound(new { Message = $"Такое сообщение не найдено" });
            }
            return Ok(res);
        }

        /// <summary>
        /// Sends a message. 
        /// </summary>
        /// <param name="sender"> Sender of message. </param>
        /// <param name="receiver"> Receiver of message. </param>
        /// <param name="topic"> Topic of message. </param>
        /// <param name="content"> The content of message. </param>
        /// <returns> New message. </returns>
        [HttpPost("send-message")]
        public IActionResult SendMessage([FromQuery] string sender, [FromQuery] string receiver, [FromQuery] string topic, [FromQuery] string content)
        {
            if (Models.User.Users.Any(x => x.Email.Equals(sender)) &&
                Models.User.Users.Any(x => x.Email.Equals(receiver)))
            {
                var message = new Message(topic, sender, receiver, content);
                Message.Messages.Add(message);
                Program.SaveData(@"Data/users.json", @"Data/messages.json");

                return Ok(message);
            }
            return BadRequest("Один из более пользователей с таким email не зарегистрирован");
        }
    }
}
