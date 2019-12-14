using Hotel_Management_Bot_Console.Commands;
using Hotel_Management_Bot_Console.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Hotel_Management_Bot_Console.Helpers
{
    static public class BotHelper
    {
        private static TelegramBotClient client;
        private static List<Command> commandList;

        public static async Task<TelegramBotClient> GetBotClient()
        {
            if (client != null)
            {
                return client;
            }

            PopulateCommands();

            client = new TelegramBotClient(AppSettings.Key);
            client.OnMessage += delegate (object sender, MessageEventArgs args)
            {

                var message = args.Message.Text;

                // could use here LUIS instead of Contains
                var command = commandList.FirstOrDefault(c => c.Contains(message));
                if (command != null)
                {
                    command.Execute(args.Message, client);
                };
            };

            return client;
        }

        private static void PopulateCommands()
        {
            commandList = new List<Command>();
            commandList.Add(new HelloComand());
            commandList.Add(new IntroductionCommand());
        }
    }
}