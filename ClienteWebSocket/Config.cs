using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteWebSocket
{
    public  class Config
    {
        public bool SslCertificateAlwaysValid { get; set; }
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }
        public int MaxConnections { get; set; }
        public string WelcomeMessage { get; set; }
        public bool ErrorMessage { get; set; }

        public static Config Load(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(json);
        }

    }
}
