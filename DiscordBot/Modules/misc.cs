using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

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
    }
}
