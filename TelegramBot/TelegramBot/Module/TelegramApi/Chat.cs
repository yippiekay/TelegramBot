using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class Chat
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
    }
}
