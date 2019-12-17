using Hotel_Management_Bot_Console.Commands;
using Hotel_Management_Bot_Console.Commands.MakeOrder;
using Hotel_Management_Bot_Console.Settings;
using System;
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
            Action initializeCommands = delegate ()
            {
                commandList = new List<Command>();
                commandList.Add(new DefaultCommand(ConsoleLogHelper.Log));
                commandList.Add(new HelloComand(ConsoleLogHelper.Log));
                commandList.Add(new IntroductionCommand(ConsoleLogHelper.Log));
                commandList.Add(new WhatToOrderCommand(ConsoleLogHelper.Log));
                commandList.Add(new ForHowManyPersonsOrderCommand(ConsoleLogHelper.Log));
                commandList.Add(new ForWhatPeriodMakeOrderCommand(ConsoleLogHelper.Log));
                commandList.Add(new CancelationCommand(ConsoleLogHelper.Log));
                commandList.Add(new ConfirmationCommand(ConsoleLogHelper.Log));
            };

            if (client != null)
            {
                return client;
            }

            initializeCommands();

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
    }
}