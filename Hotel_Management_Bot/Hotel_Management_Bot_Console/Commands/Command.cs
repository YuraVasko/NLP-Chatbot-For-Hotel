using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Hotel_Management_Bot_Console.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Execute(Message message, PredictionResponse luisResult, TelegramBotClient client);

        public bool Contains(string command)
        {
            if (!string.IsNullOrEmpty(command))
                return command.Contains(this.Name);
            else
                return false;
        }
    }
}