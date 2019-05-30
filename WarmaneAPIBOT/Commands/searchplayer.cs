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
    class SearchCommand : ModuleBase<SocketCommandContext>
    {
        public static bool stopping = false;
        [Command("search")]
        [Alias("search")]
        public async Task Handle(string serverIP, string name)
        {
            name = name.ToUpper();
            await Context.Channel.SendMessageAsync("```css\nPlease wait until I find the player. Thank you!```");
            var webClient = new WebClient();
            string jsonstring = webClient.DownloadString($"http://{serverIP}/serverlist");
            jsonstring = jsonstring.Substring(2);
            jsonstring = jsonstring.Substring(0, jsonstring.Length - 2);

            jsonstring = jsonstring.Replace("\", \"", ",");
            string[] jsonarray = jsonstring.Split(',');



            var tasks = new List<Task>();
            foreach (string element in jsonarray)
            {

                var LastTask = new Task<Task>(async () =>
                {

                    Server model = ServerAPIManager.GetServerInfo(element);
                    foreach(Player player in model.players)
                    {
                        player.name = Regex.Replace(player.name, @"(\^+((?![a-z]|[A-Z]).){0,1})+", "").ToUpper();
                        model.status.sv_hostname = Regex.Replace(model.status.sv_hostname, @"(\^+((?![a-z]|[A-Z]).){0,1})+", "");
                        if (player.name.Contains(name))
                        {
                            await Context.Channel.SendMessageAsync("```css\n" + string.Format("{0,-30}", $"[{player.name}] is playing in server", 30) + string.Format("{0,-10}", $"[{model.status.sv_hostname}] with server IP:", 30) + string.Format("{0,-10}", $"[{element}]```"));
                        }    
                    }

                    
                });
                LastTask.Start();
                tasks.Add(LastTask);
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}





