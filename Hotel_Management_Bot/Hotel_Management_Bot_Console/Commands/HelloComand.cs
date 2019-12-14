using Hotel_Management_Bot_Console.Helpers;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Hotel_Management_Bot_Console.Commands
{
    public class HelloComand : Command
    {
        public override string Name => "hello";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = "Hi! What is your name?";

            await client.SendTextMessageAsync(chatId, responseMessage, replyToMessageId: messageId);
            Console.WriteLine($"Sent message to chat with Id = { chatId }: {responseMessage}");
        }
    }
}