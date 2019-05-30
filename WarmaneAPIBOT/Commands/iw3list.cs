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
    class IW3ServerCommand : ModuleBase<SocketCommandContext>
    {
        [Command("iw3list")]
        [Alias("iw3list")]
        public async Task Handle()
        {
            var webClient = new WebClient();
            string json = webClient.DownloadString($"http://api.raidmax.org:5000/instance");
            List<T6RootObject> T6model = JsonConvert.DeserializeObject<List<T6RootObject>>(json);

            Discord.IDMChannel dm = await Context.User.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync("```css\nPlease wait until all COD4 servers with RaidMax Admin Tool are listed. Thank you!```");

            foreach (var servers in T6model.SelectMany(_model => _model.servers).Where(_model => _model.game == "IW3"))
            {
                if (servers.clientnum > 0)
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