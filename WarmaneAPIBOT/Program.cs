using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;



using WarmaneAPIBOT.Commands;



namespace WarmaneAPIBOT
{

   
    class Program
    {
        private static DiscordSocketClient client;
        private static CommandService commands;

        static void Main()
        {
            client = new DiscordSocketClient(
                new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Debug,
                });
            commands = new CommandService(
                new CommandServiceConfig
                {
                    CaseSensitiveCommands = false,


                });
            commands.AddModuleAsync(typeof(ServerinfoCommand));
            commands.AddModuleAsync(typeof(ServerListCommand));
            commands.AddModuleAsync(typeof(SearchCommand));
            commands.AddModuleAsync(typeof(SearchServerCommand));
            commands.AddModuleAsync(typeof(SearchAllPlayersCommand));
            commands.AddModuleAsync(typeof(CountCommand));
            commands.AddModuleAsync(typeof(T6ServerCommand));
            commands.AddModuleAsync(typeof(IW4ServerCommand));
            commands.AddModuleAsync(typeof(IW4SearchServerCommand));
            commands.AddModuleAsync(typeof(IW5ServerCommand));
            commands.AddModuleAsync(typeof(IW3ServerCommand));
            commands.AddModuleAsync(typeof(IW3SearchServerCommand));
            commands.AddModuleAsync(typeof(T6TotalCommand));
            commands.AddModuleAsync(typeof(T6SearchServerCommand));



            client.Log += OnLog;
            client.Ready += OnReady;
            client.MessageReceived += OnMessage;

            client.LoginAsync(TokenType.Bot, "BOT TOKEN HERE");
            client.StartAsync();


            Thread.Sleep(-1);
        }

        static async Task OnMessage(SocketMessage socketMessage)
        {
            // !bot
            SocketUserMessage socketUserMessage = (SocketUserMessage)socketMessage;
            SocketCommandContext context = new SocketCommandContext(client, socketUserMessage);

            if (context.User.IsBot)
                return;

            int position = 0;
            if (!socketUserMessage.HasCharPrefix('!', ref position))
                return;

            IResult result = await commands.ExecuteAsync(context, position);
            if (!result.IsSuccess)
                return;

        }

        static async Task OnReady()
        {

            await client.SetGameAsync("Made by Glitch", "");
            await client.SetStatusAsync(UserStatus.Online);
        }

        static async Task OnLog(LogMessage message)
        {
            Console.WriteLine($"{DateTime.Now} at {message.Source}] {message.Message}");
        }

    }
}



