using Newtonsoft.Json;
using Server.ApiClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server
{
    static class ConfigReader
    {
        public static readonly object LocalConfiguration = JsonConvert.DeserializeObject(File.ReadAllText(Directory.GetCurrentDirectory() + @"\..\..\.." + @"\conf.json"));
    }
}
