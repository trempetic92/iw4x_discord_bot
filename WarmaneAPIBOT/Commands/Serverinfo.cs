using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WarmaneAPIBOT.API;
using System.Linq;
using System.Text.RegularExpressions;

namespace WarmaneAPIBOT.Commands
{
    
    class ServerinfoCommand : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Alias("serverinfo")]
        public async Task Handle(string serverIP)
        {
            Server model = ServerAPIManager.GetServerInfo(serverIP);



            int counter = 0;
            var sw = new StringWriter();
            sw.Write($"```css\n");
            sw.WriteLine($"Players playing:\n\n");
            sw.WriteLine(String.Format("{0,-20}", "Name:", 30) + String.Format("{0,-10}", "Ping:", 30) + "Score:\n");

            foreach (Player player in model.players)
            {
                player.name = Regex.Replace(player.name, @"(\^+((?![a-z]|[A-Z]).){0,1})+", "");
                sw.WriteLine(String.Format("{0,-20}", player.name, 30) + String.Format("{0,-10}", $"[{player.ping}ms]", 30) + $"[{player.score}]");
                counter++;
            }
            sw.Write($"```");

            model.status.sv_hostname = Regex.Replace(model.status.sv_hostname, @"(\^+((?![a-z]|[A-Z]).){0,1})+", "");

            switch (model.status.g_gametype)
            {
                case "war": model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "Team Deathmatch"); break;
                case "dm": model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "Free For All"); break;
                case "koth": model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "Headquarters"); break;
                case "sd": model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "Search And Destroy"); break;
                case "gtnw": model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "Global Termonuclear War"); break;
                case "oneflag": model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "One Flag - Capture The Flag"); break;
                case "ctf": model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "Capture The Flag"); break;
                case "sab": model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "Sabotage"); break;
                case "vip": model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "VIP"); break;
                default: model.status.g_gametype = model.status.g_gametype.Replace(model.status.g_gametype, "Custom Gametype"); break;
            }

            switch (model.status.mapname)
            {
                case "mp_afghan": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Afghan"); break;
                case "mp_derail": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Derail"); break;
                case "mp_estate": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Estate"); break;
                case "mp_favela": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Favela"); break;
                case "mp_highrise": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Highrise"); break;
                case "mp_invasion": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Invasion"); break;
                case "mp_checkpoint": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Karachi"); break;
                case "mp_quarry": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Quarry"); break;
                case "mp_rundown": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Rundown"); break;
                case "mp_rust": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Rust"); break;
                case "mp_boneyard": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Scrapyard"); break;
                case "mp_nightshift": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Skidrow"); break;
                case "mp_subbase": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Sub Base"); break;
                case "mp_terminal": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Terminal"); break;
                case "mp_underpass": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Underpass"); break;
                case "mp_brecourt": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Wasteland"); break;
                case "mp_complex": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Bailout"); break;
                case "mp_crash": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Crash"); break;
                case "mp_overgrown": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Overgrown"); break;
                case "mp_compact": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Salvage"); break;
                case "mp_storm": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Storm"); break;
                case "mp_abandon": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Carnival"); break;
                case "mp_fuel2": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Fuel"); break;
                case "mp_strike": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Strike"); break;
                case "mp_trailerpark": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Trailer Park"); break;
                case "mp_vacant": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Vacant"); break;
                case "mp_nuked": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Nuketown"); break;
                case "mp_cross_fire": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Crossfire"); break;
                case "mp_bloc": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Bloc"); break;
                case "mp_cargoship": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Cargoship"); break;
                case "mp_killhouse": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Killhouse"); break;
                case "mp_bog_sh": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Bog"); break;
                case "mp_cargoship_sh": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Freighter"); break;
                case "mp_shipment_long": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Shipment: Long"); break;
                case "mp_rust_long": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Rust: Long"); break;
                case "mp_firingrange": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Firing Range"); break;
                case "mp_storm_spring": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Chemical Plant"); break;
                case "mp_fav_tropical": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Favela: Tropical"); break;
                case "mp_estate_tropical": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Estate: Tropical"); break;
                case "mp_crash_tropical": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Crash: Tropical"); break;
                case "mp_bloc_sh": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Forgotten City"); break;
                case "oilrig": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Oilrig"); break;
                case "co_hunted": model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Village"); break;
                default: model.status.mapname = model.status.mapname.Replace(model.status.mapname, "Usermap"); break;
            }

            if (String.IsNullOrEmpty(model.status.fs_game))
            {
                model.status.fs_game = "None";
            }

            if (model.status.isPrivate == "1")
            {
                model.status.isPrivate = "Yes";
            }
            else
            {
                model.status.isPrivate = "No";
            }

            if (model.status.scr_game_allowkillcam == "1")
            {
                model.status.scr_game_allowkillcam = "Yes";
            }
            else
            {
                model.status.scr_game_allowkillcam = "No";
            }

            var parts = serverIP.Split(':');
            string ipAddress = parts[0];
            string port = parts[1];
            RootObject Country = GetCountry.GetUserCountry(ipAddress);

            await Context.Channel.SendMessageAsync($"```Server location: {Country.country},{Country.city} \n\nServer name: {model.status.sv_hostname}\nGametype: {model.status.g_gametype}\n" +
                   $"Map: {model.status.mapname}\nPlayers: {counter}/{model.status.sv_maxclients}\nMod: {model.status.fs_game}\nServer Version: {model.status.shortversion}\nKillcam: {model.status.scr_game_allowkillcam}\nPassword: {model.status.isPrivate}\nSecurity Level: {model.status.sv_securityLevel}\n" +
                   $"```");

            await Context.Channel.SendMessageAsync(sw.ToString());
            await Context.Channel.SendMessageAsync($"Click to join: <iw4x://{ipAddress}:{port}>");








        }

    }

}


