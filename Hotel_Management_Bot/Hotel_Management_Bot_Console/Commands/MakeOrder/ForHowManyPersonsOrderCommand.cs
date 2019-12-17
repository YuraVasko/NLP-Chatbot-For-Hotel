﻿using Hotel_Management_Bot_Console.Helpers;
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
        public ForHowManyPersonsOrderCommand(Action<string> logFunction) : base(logFunction) { }
        public override string Name => "Make Order Second Step";

        public override async void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = string.Empty;
            string numberOfPeopleKey = "numberOfPeople";

            luisResult.Try(res =>
            {
                var peopleNumber = JsonConvert.DeserializeObject<List<string>>(string.Format("{0}", res.With(r => r.Prediction).With(p => p.Entities[numberOfPeopleKey])));
                responseMessage = $"For what period are you going to make reservation?";
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
