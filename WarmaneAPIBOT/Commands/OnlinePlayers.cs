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
    class SearchAllPlayersCommand : ModuleBase<SocketCommandContext>
    {
        public static bool stopping = false;
        [Command("allonline")]
        [Alias("allonline")]
        public async Task Handle(string serverIP)
        {
            
            await Context.Channel.SendMessageAsync("```css\nWait while i populate online player list. Thank you!```");
            var webClient = new WebClient();
            string jsonstring = webClient.DownloadString($"http://{serverIP}/serverlist");
            jsonstring = jsonstring.Substring(2);
            jsonstring = jsonstring.Substring(0, jsonstring.Length - 2);

            jsonstring = jsonstring.Replace("\", \"", ",");
            string[] jsonarray = jsonstring.Split(',');



            
            foreach (string element in jsonarray)
            {

                

                    Server model = ServerAPIManager.GetServerInfo(element);
                    foreach (Player player in model.players)
                    {
                        player.name = Regex.Replace(player.name, @"(\^+((?![a-z]|[A-Z]).){0,1})+", "").ToUpper();
                        model.status.sv_hostname = Regex.Replace(model.status.sv_hostname, @"(\^+((?![a-z]|[A-Z]).){0,1})+", "");
                        
                            await Context.Channel.SendMessageAsync("```css\n" + string.Format("{0,-30}", $"[{player.name}] is playing in server", 30) + string.Format("{0,-10}", $"[{model.status.sv_hostname}] with server IP:", 30) + string.Format("{0,-10}", $"[{element}]```"));
                    continue;
                    }


                }
                
            }
           
        }
    }






