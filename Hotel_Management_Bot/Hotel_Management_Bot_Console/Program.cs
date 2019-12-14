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
                Console.WriteLine("Bot is running");
                clientBot = await BotHelper.GetBotClient();
                clientBot.StartReceiving();

                Console.WriteLine("Type something to stop bot.");
                Console.ReadLine();
                clientBot.StopReceiving();
            }
            catch
            {
                clientBot.StopReceiving();
            }
            Console.WriteLine("Bot has been stopped running");

        }
    }
}
