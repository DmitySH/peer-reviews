using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebShet.Models;

namespace WebShet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void SaveData(string pathUsers, string pathMessages)
        {
            User.Users.Sort();

            try
            {
                JsonSerializer serializer = new JsonSerializer() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                using (StreamWriter sw = new StreamWriter(pathUsers))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, User.Users);
                    }
                }

                using (StreamWriter sw = new StreamWriter(pathMessages))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, Message.Messages);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public static void LoadData(string pathUsers, string pathMessages)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sw = new StreamReader(pathUsers))
                {
                    using (JsonReader reader = new JsonTextReader(sw))
                    {
                        User.Users = (List<User>) serializer.Deserialize(reader, typeof(List<User>));
                    }
                }

                using (StreamReader sw = new StreamReader(pathMessages))
                {
                    using (JsonReader reader = new JsonTextReader(sw))
                    {
                        Message.Messages = (List<Message>) serializer.Deserialize(reader, typeof(List<Message>));
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
