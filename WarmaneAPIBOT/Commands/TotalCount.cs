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


namespace WarmaneAPIBOT.Commands
{
    class CountCommand : ModuleBase<SocketCommandContext>
    {
        public static bool stopping = false;
        [Command("total")]
        [Alias("total")]
        public async Task Handle(string serverIP)
        {
            await Context.Channel.SendMessageAsync("```css\nPlease wait until I calculate servers and players!```");
            var webClient = new WebClient();
            string jsonstring = webClient.DownloadString($"http://{serverIP}/serverlist");
            jsonstring = jsonstring.Substring(2);jsonstring = jsonstring.Substring(0, jsonstring.Length - 2);jsonstring = jsonstring.Replace("\", \"", ",");
            string[] jsonarray = jsonstring.Split(',');

            int servercount = 0;
            int playercount = 0;

            
            for (int i = 0; i < jsonarray.Length; i++)
            {
                servercount++;
                Server model = ServerAPIManager.GetServerInfo(jsonarray[i]);
                foreach (Player player in model.players)
                {
                    playercount++;
                }
            }


            await Context.Channel.SendMessageAsync($"```Servers: {servercount} , Players: {playercount}```");
        }
    }
}





