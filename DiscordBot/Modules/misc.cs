using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.Audio;

namespace DiscordBot.Modules
{
    public class misc : ModuleBase<SocketCommandContext>
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

        
    }
}
