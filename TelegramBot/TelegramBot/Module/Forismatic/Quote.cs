using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class Quote
    {
        [JsonProperty("quoteText")]
        public string QuoteText { get; set; }

        [JsonProperty("quoteAuthor")]
        public string QuoteAuthor { get; set; }
    }
}
