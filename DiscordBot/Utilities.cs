﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace DiscordBot
{
    class Utilities
    {
        private static Dictionary<string, string> alerts;

        static Utilities()
        {
            string json = File.ReadAllText("SysLang/alerts.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            alerts = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetAlert(string key)
        {
            if (alerts.ContainsKey(key)) return alerts[key];
            return "No key found";
        }
    }
}
