using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Linq;
using Echovoice.JSON;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Collections.Specialized;


namespace WarmaneAPIBOT.API
{
    public static class GetCountry
    {
        public static RootObject GetUserCountry(string ip)
        {
            
            
                string info = new WebClient().DownloadString("http://ip-api.com/json/" + ip);
                RootObject ipInfo = JsonConvert.DeserializeObject<RootObject>(info);


            return ipInfo;
        }
    }
}
