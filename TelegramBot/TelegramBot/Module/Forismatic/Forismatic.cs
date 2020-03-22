using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class Forismatic
    {
        private readonly RestClient client = new RestClient();
        private const string url = "https://api.forismatic.com/api/1.0/?method=getQuote&format=json&lang=ru";

        public string GetRandom()
        {
            var request = new RestRequest(url);
            
            try
            {
                var response = client.Get(request);                 //запрос
                var json = response.Content;
                var quote = JsonConvert.DeserializeObject<Quote>(json);

                if (quote.QuoteAuthor.Length > 0)
                {
                    return $"Мудрый {quote.QuoteAuthor} однажды сказал: \"{quote.QuoteText}\"";
                }
                else
                {
                    return $"Кто-то однажды сказал: {quote.QuoteText}";
                }
            }
            catch (Exception )
            {
                return "Error! Trouble with connection.\n Try again";
            }
        }
    }
}
