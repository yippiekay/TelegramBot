using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelegramBot
{
    public class NBRBCurrencyExchange
    {
        private static readonly RestClient client = new RestClient();

        private const string url = "http://www.nbrb.by/api/exrates/rates?periodicity=0";
        readonly List<Currency> listOfExchangeRates = GetListOfExchangeRates();
        
        public string GetRate(string requestOfRate)
        {
            if (listOfExchangeRates == null) { return "Error! \nExchange rates haven't been receieved \nTry again"; }
            if (requestOfRate.ToLower().Contains("all")) { return GetAllRate(); }

            string rate = GetCurrencyRate(requestOfRate);
            return rate;
        }

        private static List<Currency> GetListOfExchangeRates()
        {
            List<Currency> listOfExchangeRates;

            try
            {
                var request = new RestRequest(url);
                var response = client.Get(request);
                var json = response.Content;
                listOfExchangeRates = JsonConvert.DeserializeObject<List<Currency>>(json);
            }
            catch (Exception)
            {
                return null;
            }
            return listOfExchangeRates;
        }

        private string GetAllRate()
        {
            var answers = new List<string>();

            foreach (var rate in listOfExchangeRates) 
                answers = listOfExchangeRates.Select(rate => $"{rate.CurrencyScale} {rate.CurrencyName} - {rate.CurrencyOfficialRate} Рубля(-ей)").ToList();

            return string.Join("\n", answers);
        }

        private string GetCurrencyRate(string requestOfRate)
        {
            var answer = from currency in listOfExchangeRates
                         where requestOfRate.ToUpper().Contains(currency.CurrencyAbbreviation.ToUpper()) 
                         select $"{currency.CurrencyScale} {currency.CurrencyName} - {currency.CurrencyOfficialRate} Рубля(-ей)";
            if (answer.Count() == 0) { return "Unknow currency!"; }

            return string.Join("\n", answer); 
        }
    }
}
