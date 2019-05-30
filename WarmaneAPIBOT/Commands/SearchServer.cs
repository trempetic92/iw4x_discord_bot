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
    class SearchServerCommand : ModuleBase<SocketCommandContext>
    {
        public static bool stopping = false;
        [Command("country")]
        [Alias("country")]
        public async Task Handle(string serverIP, params string[] country_sh)
        {
            await Context.Channel.SendMessageAsync("```css\nPlease wait until all the servers from location u entered are listed. Thank you!```");
            var webClient = new WebClient();
            string jsonstring = webClient.DownloadString($"http://{serverIP}/serverlist");
            Console.WriteLine(jsonstring);
            jsonstring = jsonstring.Substring(2);
            Console.WriteLine(jsonstring);
            jsonstring = jsonstring.Substring(0, jsonstring.Length - 2);
            Console.WriteLine(jsonstring);

            jsonstring = jsonstring.Replace("\", \"", ",");
            string[] jsonarray = jsonstring.Split(',');
            Console.WriteLine(jsonstring);

            string countryname = country_sh
                .Select(w => char.ToUpper(w[0]) + w.Substring(1))
                .Aggregate((a, b) => a + "+" + b);

            var tasks = new List<Task>();
            foreach (string element in jsonarray)
            {

                var LastTask = new Task<Task>(async () =>
                {

                    Server model = ServerAPIManager.GetServerInfo(element);

                    var parts = element.Split(':');
                    string ipAddress = parts[0];
                    string port = parts[1];

                    RootObject Country = GetCountry.GetUserCountry(ipAddress);
                    
                        if (Country.countryCode == countryname)
                        {
                            var sw = new StringWriter();
                            sw.Write($"```css\n");
                            sw.WriteLine(string.Format("{0,-30}", "IP:", 30) + string.Format("{0,-60}", "Server name:", 30) + string.Format("{0,-10}", "Players:", 30) + "Country:\n");
                            model.status.sv_hostname = Regex.Replace(model.status.sv_hostname, @"(\^+((?![a-z]|[A-Z]).){0,1})+", "");
                            sw.WriteLine(string.Format("{0,-30}", $"[{element}]", 30) + string.Format("{0,-60}", $"[{model.status.sv_hostname}]", 30) + string.Format("{0,-10}", $"[{model.players.Count}/{model.status.sv_maxclients}]") + $"[{Country.country},{Country.city}]");

                            sw.Write($"```");
                            await Context.Channel.SendMessageAsync(sw.ToString());
                        }
                    
                });
                LastTask.Start();
                tasks.Add(LastTask);
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}





