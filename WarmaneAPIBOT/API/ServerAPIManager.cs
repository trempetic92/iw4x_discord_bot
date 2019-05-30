using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Linq;

namespace WarmaneAPIBOT.API
{
    public static class ServerAPIManager
    {
        public static Server GetServerInfo(string serverIP)
        {

            var webClient = new WebClient();
            string json = webClient.DownloadString($"http://{serverIP}/info");

            Server model = JsonConvert.DeserializeObject<Server>(json);
            return model;
        }
    }
}