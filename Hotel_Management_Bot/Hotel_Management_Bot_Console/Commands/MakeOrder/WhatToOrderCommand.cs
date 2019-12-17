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
    class WhatToOrderCommand : Command
    {
        public WhatToOrderCommand(Action<string> logFunction) : base(logFunction) { }
        public override string Name => "Make Order First Step";

        public override async void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = string.Empty;
            string whatToOrderKey = "whatToOrder";

            luisResult.Try(res =>
            {
                var whatToOrder = JsonConvert.DeserializeObject<List<string>>(string.Format("{0}", res.With(r=>r.Prediction).With(p=>p.Entities[whatToOrderKey])));
                // Save what person are ging to order to DB.
                responseMessage = $"For how many persons are you going to book {whatToOrder[0]}?";
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
