using Hotel_Management_Bot_Console.Helpers;
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
        public override string Name => "name";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = string.Empty;

            var prediction = LuisHelper.GetPredictionAsync(message.Text).Result;
            try
            {
                var personName = JsonConvert.DeserializeObject<List<string>>(string.Format("{0}", prediction.Prediction.Entities["personName"]));
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
