using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class ApiResult
    {
        [JsonProperty("result")]
        public Update[] Result { get; set; }
    }
}
