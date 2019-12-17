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
        public override string Name => "Make Order First Step";

        public override async void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = string.Empty;

            try
            {
                var whatToOrder = JsonConvert.DeserializeObject<List<string>>(string.Format("{0}", luisResult.Prediction.Entities["whatToOrder"]));
                // Save what person are ging to order to DB.
                responseMessage = $"For how many persons are you going to book {whatToOrder[0]}?";
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
