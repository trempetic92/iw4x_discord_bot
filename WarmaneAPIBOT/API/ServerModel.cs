using System;
using System.Collections.Generic;
using System.Text;

namespace WarmaneAPIBOT.API
{

    public class Server
    {
        public List<Player> players { get; set; }
        public Status status { get; set; }
    }

    public class Player
    {
        public string name { get; set; }
        public int ping { get; set; }
        public int score { get; set; }
    }

    public class Status
    {
        public string checksum { get; set; }
        public string fs_game { get; set; }
        public string g_gametype { get; set; }
        public string g_hardcore { get; set; }
        public string gamename { get; set; }
        public string isPrivate { get; set; }
        public string mapname { get; set; }
        public string matchtype { get; set; }
        public string protocol { get; set; }
        public string scr_game_allowkillcam { get; set; }
        public string scr_team_fftype { get; set; }
        public string shortversion { get; set; }
        public string sv_allowAnonymous { get; set; }
        public string sv_allowClientConsole { get; set; }
        public string sv_floodProtect { get; set; }
        public string sv_hostname { get; set; }
        public string sv_maxPing { get; set; }
        public string sv_maxRate { get; set; }
        public string sv_maxclients { get; set; }
        public string sv_minPing { get; set; }
        public string sv_privateClients { get; set; }
        public string sv_privateClientsForClients { get; set; }
        public string sv_pure { get; set; }
        public string sv_securityLevel { get; set; }
    }
    

}
