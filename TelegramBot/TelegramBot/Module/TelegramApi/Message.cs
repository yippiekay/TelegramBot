using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class Message
    {
        [JsonProperty("chat")]
        public Chat Chat { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("date")]
        public int Date { get; set; }
    }
}
