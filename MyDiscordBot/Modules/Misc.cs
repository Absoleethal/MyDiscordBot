using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MyDiscordBot.Core.UserAccounts;

namespace MyDiscordBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("myStats")]
        public async Task MyStats()
        {
            var account = UserAccounts.GetAccount(Context.User);
            var embed = new EmbedBuilder();

            embed.WithColor(new Color(128, 255, 0));
            embed.WithThumbnailUrl("https://vignette.wikia.nocookie.net/bighero6botfight/images/0/0a/0048.PNG/revision/latest?cb=20141107093851");

            await Context.Channel.SendMessageAsync($"You have {account.XP} XP and {account.Points} points",false, embed);
        }

        [Command("addXP")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task addXP(uint xp)
        {
            var account = UserAccounts.GetAccount(Context.User);
            account.XP += xp;
            UserAccounts.SaveAccounts();

            var embed = new EmbedBuilder();

            embed.WithColor(new Color(128, 255, 0));
            embed.WithThumbnailUrl("https://vignette.wikia.nocookie.net/bighero6botfight/images/0/0a/0048.PNG/revision/latest?cb=20141107093851");

            await Context.Channel.SendMessageAsync($"You gained {xp} XP.", false, embed);
        }



        [Command("mank")]
        public async Task Mank()
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("Mank was bullied by: " +Context.User.Username);
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

        [Command("secret")]
        public async Task SecretMsg([Remainder] string arg = "")
        {
            if (!UserIsMOD((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync(":X: You need to complete the Quest " + Context.User.Mention);
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }

        private bool UserIsMOD(SocketGuildUser user)
        {
            //user.Guild.Roles
            string targetRoleName = "MOD";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
        [Command("data")]
        public async Task GetData()
        {

            await Context.Channel.SendMessageAsync("Data has: " + DataStorage.GetPairsCount() + " pairs.");
            DataStorage.AddPairToStorage("Count" + DataStorage.GetPairsCount(), "TheCount" + DataStorage.GetPairsCount());
        }
        
    }
}
