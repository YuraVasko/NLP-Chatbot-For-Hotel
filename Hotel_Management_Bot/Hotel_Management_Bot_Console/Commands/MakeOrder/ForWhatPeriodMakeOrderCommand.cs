using Hotel_Management_Bot_Console.Helpers;
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
        public ForWhatPeriodMakeOrderCommand(Action<string> logFunction) : base(logFunction) { }
        public override string Name => "Make Order Third Step";

        public override async void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = string.Empty;

            string orderTimeKey = "orderTime";

            luisResult.Try(res =>
            {
                //DateTime parsing
                //var dateTimeData = JsonConvert.DeserializeObject<List<string>>(string.Format("{0}", res.Prediction.Entities["orderTime"]));
                //Save date to DB.
                responseMessage = $"Are you sure u want to confirm your order?";
            },
            res =>
            {
                responseMessage = $"I do not get you. Could you please repeat?";
            });

            await client.SendTextMessageAsync(chatId, responseMessage, replyToMessageId: messageId);
            _logFunction($"Sent message to chat with Id = { chatId }: {responseMessage}");
        }
    }
}
