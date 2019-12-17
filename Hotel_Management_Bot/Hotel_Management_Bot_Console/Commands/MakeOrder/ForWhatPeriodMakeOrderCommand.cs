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
    class ForWhatPeriodMakeOrderCommand : Command
    {
        public override string Name => "Make Order Third Step";

        public override async void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = string.Empty;

            try
            {
                //DateTime parsing
                //var dateTimeData = JsonConvert.DeserializeObject<List<string>>(string.Format("{0}", luisResult.Prediction.Entities["orderTime"]));
                //Save date to DB.
                responseMessage = $"Are you sure u want to confirm your order?";
            }
            catch (Exception)
            {
                responseMessage = $"Sorry, error has been occured?";
            }
            await client.SendTextMessageAsync(chatId, responseMessage, replyToMessageId: messageId);
            Console.WriteLine($"Sent message to chat with Id = { chatId }: {responseMessage}");
        }
    }
}
