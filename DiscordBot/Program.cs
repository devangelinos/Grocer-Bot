using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;


namespace DiscordBot
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;
        private IServiceProvider _services;

        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            //_services = new IServiceCollection().AddSingleton(new AudioService());
            _client.Log += Log;
            _client.ReactionAdded += OnReactionAdded;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }

        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> cache, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (reaction.MessageId == Global.MessageIdToTrack)
            {
                if (reaction.Emote.Name == "mpeto")
                {
                    var embed = new EmbedBuilder();
                    embed.WithTitle("ΤΙ ΚΑΝΕΙΙ");
                    embed.WithDescription(" Μη με μπετώνεις ρε " + reaction.User.Value.Username);
                    embed.WithColor(new Color(255, 153, 51));
                    await channel.SendMessageAsync("", false, embed);
                }
            }
            
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
            
        }
    }
}
