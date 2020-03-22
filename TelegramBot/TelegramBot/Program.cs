using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TelegramBot
{
    class Program
    {
        private static Dictionary<string, string> commandToAnswer;

        static void Main()
        { 
            TelegramAPI api = new TelegramAPI();
            string path = @$"{Environment.CurrentDirectory}\commandToAnswer.json";
            var data = File.ReadAllText(path);
            commandToAnswer = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            while (true)
            {
                var updates = api.GetUpdates();
                if( updates == null) { continue; }

                foreach (var update in updates)
                {
                    if ( string.IsNullOrEmpty(update.Message.Text) ||  update.UpdateId == null) { continue; }

                    string answer = AnswerQuestion(update.Message.Text);
                    
                    api.SendMessage(answer, update.Message.Chat.Id);
                    
                    Thread.Sleep(4000);
                }
            }   
        }

        private static string AnswerQuestion(string userQuestion)
         {
            var answers = new List<string>();
            var question = userQuestion.ToLower();

            foreach (var entry in commandToAnswer)
            {
                if (IsContain(entry.Key, question))
                {
                    answers.Add(entry.Value);
                }
            }

            if(question.Contains("time"))
            {
                var time = DateTime.Now.ToString("HH:mm:ss");
                answers.Add("Current time is " + time);
            }

            if (question.Equals("/random_quote"))
            {
                var forismatic = new Forismatic();
                answers.Add(forismatic.GetRandom());
            }

            if(question.Contains("currenc"))
            {
                var nbrbCurrencyExchange = new NBRBCurrencyExchange();
                answers.Add(nbrbCurrencyExchange.GetRate(question));
            }

            if (answers.Count == 0)
            {
                answers.Add("Я еще только развиваюсь и не знаю ответа, спроси что-нибудь еще :)");
            }

            return string.Join(", ", answers);
        }

        private static bool IsContain(string phrase, string text)
        {
            string[] words = phrase.Split();
            foreach(string word in words)
            {
                if(!text.Contains(word)) { return false; }
            }
            return true;
        }
    }
}
