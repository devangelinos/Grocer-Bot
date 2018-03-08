using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.Audio;
using Discord.Rest;
using YoutubeExtractor;
using System.IO;
using System.Text.RegularExpressions;

namespace DiscordBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("Echo")]
        public async Task Echo([Remainder] string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed Message");
            embed.WithDescription(message);
            embed.WithColor(new Color(255, 153, 51));

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("join")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            
            if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            var audioClient = await channel.ConnectAsync();
        }

        [Command("react")]
        public async Task HandleReaction()
        {
            RestUserMessage msg = await Context.Channel.SendMessageAsync("React to me");
            Global.MessageIdToTrack = msg.Id;

        }

        [Command("Create lobby")]
        public async Task CreateLobby([Remainder] string message)
        {
            
            //Regular expression to check if the message's format after the command is correct
            Regex Re = new Regex(@"^\s*(\S+\s*)+\-\d{1}\/\d{1}$");
            if (Re.IsMatch(message)){

                //Getting the game's name
                int _lastIndex = message.IndexOf("-")-1;
                string primary = message.Substring(0 , _lastIndex);
                //Getting the current people and the max people for the lobby
                string secondary = message.Substring(_lastIndex + 1);
                int[] numbers = (from Match m in Regex.Matches(secondary, @"\d+") select int.Parse(m.Value)).ToArray();
                

                var embed = new EmbedBuilder();
                embed.WithTitle(Context.User.Username + " created a lobby for " + primary);
                embed.WithDescription(numbers[0] + "/" + numbers[1]);
                embed.WithColor(new Color(255, 153, 51));

                await Context.Channel.SendMessageAsync("", false, embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Wrong command");
                embed.WithDescription("Example: >Make lobby Discord -1/4");
                embed.WithColor(new Color(255, 153, 51));

                await Context.Channel.SendMessageAsync("", false, embed);
            }
            
        }

    }
}
