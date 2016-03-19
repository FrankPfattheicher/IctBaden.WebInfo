using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace IctBaden.WebInfo.Browser
{
    internal class Section : Dictionary<string, string>
    {
    }
    internal class IniFile : Dictionary<string, Section>
    {
        private static readonly Regex SectionFmt = new Regex(@"^\[(.*)\]", RegexOptions.Compiled);
        private static readonly Regex KeyFmt = new Regex(@"^([^=]+)=(.*)$", RegexOptions.Compiled);

        private void LoadContent(TextReader content)
        {
            var currentSection = new Section();
            Add(" ", currentSection);

            while (true)
            {
                var line = content.ReadLine();
                if (line == null) break;

                if (string.IsNullOrEmpty(line))
                    continue;

                var isSection = SectionFmt.Match(line);
                if (isSection.Success)
                {
                    var name = isSection.Groups[1].Value;
                    currentSection = new Section();
                    Add(name, currentSection);
                    continue;
                }

                var isKey = KeyFmt.Match(line);
                if (!isKey.Success)
                    continue;

                var key = isKey.Groups[1].Value;
                var value = isKey.Groups[2].Value;
                currentSection.Add(key, value);
            }
        }

        public static IniFile FromFile(string fileName)
        {
            var iniFile = new IniFile();
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
                return null;

            using (var fileData = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var fileStream = new StreamReader(fileData))
                {
                    iniFile.LoadContent(fileStream);
                }
            }
            return iniFile;
        }

        public static IniFile FromResource(Assembly assembly, string resourceName)
        {
            var iniFile = new IniFile();

            resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(resourceName));
            if (resourceName == null) return null;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;

                // Assembly resources are stored in default (Windows) encoding as the files are.
                using (var reader = new StreamReader(stream))
                {
                    iniFile.LoadContent(reader);
                }
            }
            return iniFile;
        }

    }
}