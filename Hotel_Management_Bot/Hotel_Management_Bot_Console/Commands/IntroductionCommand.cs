﻿using Hotel_Management_Bot_Console.Helpers;
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

        public override async void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = string.Empty;

            try
            {
                var personName = JsonConvert.DeserializeObject<List<string>>(string.Format("{0}", luisResult.Prediction.Entities["personName"]));
                responseMessage = $"Hi {personName.FirstOrDefault()}, How can I help you?";
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
