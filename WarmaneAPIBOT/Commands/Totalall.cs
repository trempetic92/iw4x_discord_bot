using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WarmaneAPIBOT.API;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Echovoice.JSON;
using Discord;
using Discord.WebSocket;
using System.Diagnostics;
using Discord.Net;
using Newtonsoft.Json;
using System.Linq;

namespace WarmaneAPIBOT.Commands
{
    class T6TotalCommand : ModuleBase<SocketCommandContext>
    {
        [Command("servers")]
        [Alias("servers")]
        public async Task Handle(string game)
        {
            var webClient = new WebClient();
            string json = webClient.DownloadString($"http://api.raidmax.org:5000/instance");
            List<T6RootObject> T6model = JsonConvert.DeserializeObject<List<T6RootObject>>(json);
            Console.WriteLine(T6model.ToString());

            game = game.ToLower();

            string a = "servers";
            string b = "server";

            int counter = 0;

            if (game == "t6" || game == "t6m")
            {

                foreach (var servers in T6model.SelectMany(_model => _model.servers).Where(_model => _model.game == "T6" || _model.game == "T6M"))
                {
                    counter++;
                }
                if (counter > 0)
                {
                    await Context.Channel.SendMessageAsync($"```css\nThere are [{counter}] BO2 {a} with RaidMax Admin Tool online.\n\nType [!t6list] to see non-empty servers.\n```");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"```css\nThere are [{counter}] BO2 {b} with RaidMax Admin Tool online.\n\nType [!t6list] to see non-empty servers.\n```");
                }
            }
            else if (game == "iw4")
            {

                foreach (var servers in T6model.SelectMany(_model => _model.servers).Where(_model => _model.game == "IW4"))
                {
                    counter++;
                }
                if (counter > 0)
                {
                    await Context.Channel.SendMessageAsync($"```css\nThere are [{counter}] MW2 {a} with RaidMax Admin Tool online.\n\nType [!iw4list] to see non-empty servers.\n```");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"```css\nThere are [{counter}] MW2 {b} with RaidMax Admin Tool online.\n\nType [!iw4list] to see non-empty servers.\n```");
                }
            }
            else if (game == "iw3")
            {

                foreach (var servers in T6model.SelectMany(_model => _model.servers).Where(_model => _model.game == "IW3"))
                {
                    counter++;
                }
                if (counter > 0)
                {
                    await Context.Channel.SendMessageAsync($"```css\nThere are [{counter}] COD4 {a} with RaidMax Admin Tool online.\n\nType [!iw3list] to see non-empty servers.\n```");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"```css\nThere are [{counter}] COD4 {b} with RaidMax Admin Tool online.\n\nType [!iw3list] to see non-empty servers.\n```");
                }
            }
        }
    }
}
