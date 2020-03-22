using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public class TelegramAPI
    {
        public void SendMessage(string text, int chatId)
        {
            SendApiRequest("SendMessage", $"chat_id={chatId}&text={text}");
        }

        private int lastUpdateId = 0;

        public Update[] GetUpdates()
        {
            var json = SendApiRequest("GetUpdates", $"offset={lastUpdateId}");
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(json);

            try
            {
                foreach (var update in apiResult.Result)
                {
                    lastUpdateId = (int)update.UpdateId + 1;

                    Console.WriteLine($"{Time(update.Message.Date)}, id чата: {update.Message.Chat.Id}, Имя {update.Message.Chat.FirstName}, текст сообщения: {update.Message.Text} ");
                }
                return apiResult.Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static DateTime Time(int unixTime)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return time.AddSeconds(unixTime);
        }
       
        private string SendApiRequest(string apiMethod, string param)
        {
            RestClient client = new RestClient();
            string apiURL = "https://api.telegram.org/bot" + SecretKey.apiKey + "/";

            var url = apiURL + apiMethod + "?" + param;
            var request = new RestRequest(url);                     //объект запроса
            try
            {
                var response = client.Get(request);                 //запрос
                return response.Content;                            //возращение результата запроса
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
