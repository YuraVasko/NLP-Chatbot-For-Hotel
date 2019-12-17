using Hotel_Management_Bot_Console.Helpers;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Hotel_Management_Bot_Console.Commands
{
    class IntroductionCommand : Command
    {
        public override string Name => "Introduction";
        public IntroductionCommand(Action<string> logFunction) : base(logFunction) { }

        public override async void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = string.Empty;

            luisResult.Try(res => 
            {
                var personName = JsonConvert.DeserializeObject<List<string>>(string.Format("{0}", res.With(r=>r.Prediction).With(p=>p.Entities["personName"])));
                responseMessage = $"Hi {personName.FirstOrDefault()}, How can I help you? Here you could book roo, SPA or restaurant!";
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
