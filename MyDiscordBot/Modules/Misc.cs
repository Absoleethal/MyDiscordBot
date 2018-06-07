using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace MyDiscordBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        

        [Command("mank")]
        public async Task Mank()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Mank is bullied by: " +Context.User.Username);
            embed.WithColor(new Color (128,255,0));
            embed.WithThumbnailUrl("https://vignette.wikia.nocookie.net/bighero6botfight/images/0/0a/0048.PNG/revision/latest?cb=20141107093851");

            await Context.Channel.SendMessageAsync("Mank es una rana culia, y verde tambien!",false,embed);
        }

        [Command("echo")]
        public async Task Echo([Remainder] string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message by: " +Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Color(128, 255, 0));
            embed.WithThumbnailUrl("https://vignette.wikia.nocookie.net/bighero6botfight/images/0/0a/0048.PNG/revision/latest?cb=20141107093851");

            await Context.Channel.SendMessageAsync("",false, embed);

        }

        [Command("pick")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' } , StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(128, 255, 0));
            embed.WithThumbnailUrl("https://vignette.wikia.nocookie.net/bighero6botfight/images/0/0a/0048.PNG/revision/latest?cb=20141107093851");

            await Context.Channel.SendMessageAsync("", false, embed);

        }

        [Command("read")]
        public async Task Readit([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithDescription(message);
            embed.WithColor(new Color(128, 255, 0));
            embed.WithThumbnailUrl("https://vignette.wikia.nocookie.net/bighero6botfight/images/0/0a/0048.PNG/revision/latest?cb=20141107093851");

            await Context.Channel.SendMessageAsync(message, true, embed);
        }
        
        [Command("help")]
        public async Task Helpme()
        {

            await Context.Channel.SendMessageAsync(Utilities.GetAlert("Help")); //Reminder, change the alert json to input help descriptions
        }


    }
}
