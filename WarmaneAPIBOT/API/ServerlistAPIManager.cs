using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Linq;
using Echovoice.JSON;

namespace WarmaneAPIBOT.API
{
    public static class ServerlistAPIManager
    {
        public static Root GetServerlistInfo(string serverIP)
        {

            var webClient = new WebClient();
            string json = webClient.DownloadString($"http://{serverIP}/serverlist");

            Root model = JsonConvert.DeserializeObject<Root>(json);
            
       

            return model;


        }
    }
}