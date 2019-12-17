using Hotel_Management_Bot_Console.Commands;
using Hotel_Management_Bot_Console.Commands.MakeOrder;
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
                if(!string.IsNullOrEmpty(message))
                {
                    var prediction = LuisHelper.GetPredictionAsync(message).Result;
                    var intentName = prediction.Prediction.TopIntent;
                    Command command = commandList.FirstOrDefault(c => c.Name.ToLower() == intentName.ToLower());

                    if(command == null)
                    {
                        command = commandList.FirstOrDefault(c => c is DefaultCommand);
                    }

                    command.Execute(args.Message, prediction, client);
                }     
            };

            return client;
        }

        private static void PopulateCommands()
        {
            commandList = new List<Command>();
            commandList.Add(new DefaultCommand());
            commandList.Add(new HelloComand());
            commandList.Add(new IntroductionCommand());
            commandList.Add(new WhatToOrderCommand());
            commandList.Add(new ForHowManyPersonsOrderCommand());
            commandList.Add(new ForWhatPeriodMakeOrderCommand());
            commandList.Add(new CancelationCommand());
            commandList.Add(new ConfirmationCommand());
        }
    }
}