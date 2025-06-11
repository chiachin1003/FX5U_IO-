using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Config
{
    public class DbConfig
    {
        public static DbConfigSection Local { get; private set; }
        public static DbConfigSection Cloud { get; private set; }

        public static void LoadFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var config = JsonConvert.DeserializeObject<Dictionary<string, DbConfigSection>>(json);

            Local = config["Local"];
            Cloud = config["Cloud"];
        }
    }
    public class DbConfigSection
    {
        public string IpAddress { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
