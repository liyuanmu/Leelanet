using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Leelanet.Config
{
    public class LeelaConfig
    {
        public static string Centisec;
        public static string LeelaLoction;
        public static string Arguments;
        public static void Init()
        {
            Centisec = GetSettingS("analyze-update-interval-centisec");
            LeelaLoction = GetSettingS("LeelaLocation");
            Arguments = GetSettingS("Arguments");
        }
        private static string GetSettingS(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
