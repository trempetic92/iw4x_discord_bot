using System;
using System.Collections.Generic;
using System.Text;

namespace WarmaneAPIBOT.API
{
    public class T6Server
    {
        public object id { get; set; }
        public string ip { get; set; }
        public string hostname { get; set; }
        public string gametype { get; set; }
        public string map { get; set; }
        public string version { get; set; }
        public int port { get; set; }
        public int clientnum { get; set; }
        public string game { get; set; }
        public int maxclientnum { get; set; }
    }

    public class T6RootObject
    {
        public string id { get; set; }
        public int last_heartbeat { get; set; }
        public int uptime { get; set; }
        public double version { get; set; }
        public List<T6Server> servers { get; set; }
    }
}
