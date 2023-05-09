using Interop.UIAutomationClient;
using Telegram.Bot;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using YaPlayer.Model.Classes;
using YaPlayer.Model.Interfaces;
using System.Configuration;

namespace YaPlayer
{
    class Setup
    {
        static YaPlayerAutomationProvider? _automationProvider;
        static TelegramBotClient? _bot;
        static CommandsExecutor? _executor;
        static ICommandsFactory? _commandFactory;
        static string _token = ConfigurationManager.AppSettings["Token"]!;

        static async Task Main(string[] args)
        {
            _commandFactory = new DefaultCommandsFactory();
            _automationProvider = new YaPlayerAutomationProvider();
            _bot = new TelegramBotClient(_token);
            _executor = new CommandsExecutor(_commandFactory, _automationProvider);

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() 
            };

            _bot.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
            Console.ReadLine();
            cts.Cancel();
        }

        async static Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
            {
                return;
            }

            if (message.Text is not { } messageText)
            {
                return;
            }
            _executor.HandleCommand(messageText);
        }

        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
