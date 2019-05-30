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
    class IW4SearchServerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("iw4search")]
        [Alias("iw4search")]
        public async Task Handle(string servername)
        {
            servername = servername.ToUpper();
            var webClient = new WebClient();
            string json = webClient.DownloadString($"http://api.raidmax.org:5000/instance");
            List<T6RootObject> T6model = JsonConvert.DeserializeObject<List<T6RootObject>>(json);

            Discord.IDMChannel dm = await Context.User.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync($"```css\nPlease wait until all MW2 servers with RaidMax Admin Tool that contain word {servername} are listed. Thank you!```");

            foreach (var servers in T6model.SelectMany(_model => _model.servers).Where(_model => _model.game == "IW4"))
            {
                
                if (servers.hostname.Contains(servername) )
                {
                    RootObject Country = GetCountry.GetUserCountry(servers.ip);
                    Discord.IDMChannel dn = await Context.User.GetOrCreateDMChannelAsync();
                    await dn.SendMessageAsync($"```css\nServer location: [{Country.country},{Country.city}] \nServer name: [{servers.hostname}]\nGametype: [{servers.gametype}]\n" +
                        $"Map: [{servers.map}]\nPlayers: [{servers.clientnum}/{servers.maxclientnum}]\n\nServer IP: [{servers.ip}:{servers.port}]```");
                }
            }

        }
    }
}