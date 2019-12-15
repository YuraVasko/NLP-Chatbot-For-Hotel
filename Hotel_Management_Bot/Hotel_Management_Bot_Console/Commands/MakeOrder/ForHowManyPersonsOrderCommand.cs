using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Hotel_Management_Bot_Console.Commands.MakeOrder
{
    class ForHowManyPersonsOrderCommand : Command
    {
        public override string Name => "Make Order Second Step";

        public override async void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = string.Empty;

            try
            {
                var peopleNumber = JsonConvert.DeserializeObject<List<string>>(string.Format("{0}", luisResult.Prediction.Entities["numberOfPeople"]));
                //Save people number to DB.
                responseMessage = $"For what period are you going to make reservation?";
            }
            catch (Exception)
            {
                responseMessage = $"Sorry, error has been occured";
            }
            await client.SendTextMessageAsync(chatId, responseMessage, replyToMessageId: messageId);
            Console.WriteLine($"Sent message to chat with Id = { chatId }: {responseMessage}");
        }
    }
}
