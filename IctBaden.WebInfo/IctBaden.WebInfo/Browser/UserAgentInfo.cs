using System;
using System.Collections.Generic;

namespace IctBaden.WebInfo.Browser
{
    public class UserAgentInfo : Dictionary<string, string>
    {
        public string Get(string key)
        {
            return ContainsKey(key) ? this[key] : String.Empty;
        }

        public string Platform => Get("Platform");
        public string Browser => Get("Browser");
        public string Version => Get("Version");
        public string DeviceType => Get("Device_Type");
        public bool IsMobile => Get("isMobileDevice") == "true";
        public bool IsTablet => Get("isTablet") == "true";
        public bool IsCrawler => Get("Crawler") == "true";
    }
}