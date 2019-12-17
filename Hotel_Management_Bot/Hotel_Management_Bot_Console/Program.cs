using Hotel_Management_Bot_Console.Helpers;
using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Hotel_Management_Bot_Console
{
    class Program
    {
        static TelegramBotClient clientBot;
        static async Task Main(string[] args)
        {
            try
            {
                ConsoleLogHelper.Log("Bot is running");
                clientBot = await BotHelper.GetBotClient();
                clientBot.StartReceiving();

                ConsoleLogHelper.Log("Type something to stop bot.");
                Console.ReadLine();
                clientBot.StopReceiving();
            }
            catch
            {
                clientBot.StopReceiving();
            }
            ConsoleLogHelper.Log("Bot has been stopped running");

        }
    }
}
