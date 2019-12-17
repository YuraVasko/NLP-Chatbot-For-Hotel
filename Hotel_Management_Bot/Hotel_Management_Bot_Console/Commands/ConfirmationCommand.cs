using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Hotel_Management_Bot_Console.Commands
{
    class ConfirmationCommand : Command
    {
        public ConfirmationCommand(Action<string> logFunction) : base(logFunction) { }
        public override string Name => "Confirmation";

        public override async void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string responseMessage = "Great! Your action has been successfully done. Anything else?";

            await client.SendTextMessageAsync(chatId, responseMessage, replyToMessageId: messageId);
            _logFunction($"Sent message to chat with Id = { chatId }: {responseMessage}");
        }
    }
}
