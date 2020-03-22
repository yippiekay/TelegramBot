using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class Update
    {
        [JsonProperty("update_id")]
        public int? UpdateId { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }
    }
}
