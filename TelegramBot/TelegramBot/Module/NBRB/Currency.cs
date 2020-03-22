using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class Currency
    {
        [JsonProperty("Cur_Abbreviation")]
        public string CurrencyAbbreviation { get; set; }

        [JsonProperty("Cur_Name")]
        public string CurrencyName { get; set; }
        
        [JsonProperty("Cur_OfficialRate")]
        public string CurrencyOfficialRate { get; set; }
        
        [JsonProperty("Cur_Scale")]
        public string CurrencyScale { get; set; }
    }
}
