using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IctBaden.WebInfo.Browser
{
    public class UserAgent
    {
        private static IniFile browscap;

        public static UserAgentInfo Parse(string userAgentString)
        {
            if (browscap == null)
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".";
                browscap = IniFile.FromFile(Path.Combine(path, "browscap.ini"));
            }
            if (browscap == null)
            {
                browscap = IniFile.FromResource(Assembly.GetExecutingAssembly(), "lite_asp_browscap.ini");
            }

            var info = new UserAgentInfo();
            var match = FindMatch(userAgentString);
            do
            {
                var entries = browscap[match].ToArray();
                match = "";
                foreach (var entry in entries)
                {
                    if (entry.Key == "Parent")
                    {
                        match = entry.Value;
                    }
                    else if (!info.ContainsKey(entry.Key))
                    {
                        info.Add(entry.Key, entry.Value);
                    }
                }
            } while (match != "");

            return info;
        }

        private static string FindMatch(string agent)
        {
            foreach (var section in browscap.Keys)
            {
                if ((section.IndexOfAny(new[] { '*', '?' }) == -1) && (section == agent))
                {
                    // prevent matching of group entries
                    if ((browscap[section].ContainsKey("Parent") ? browscap[section]["Parent"] : string.Empty) != "DefaultProperties")
                    {
                        return section;
                    }
                }
            }
            foreach (var section in browscap.Keys)
            {
                try
                {
                    if (section.IndexOfAny(new[] { '*', '?' }) > -1)
                    {
                        var regex = Regex.Escape(section).Replace(@"\*", ".*").Replace(@"\?", ".");
                        if (Regex.IsMatch(agent, "^" + regex + "$"))
                        {
                            return section;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return "*";
        }

    }
}
