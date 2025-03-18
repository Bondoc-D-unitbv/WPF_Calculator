using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MVP_Calc_V3
{
    public class SettingsManager
    {
        private static readonly string settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.txt");

        public static Dictionary<string, string> LoadSettings()
        {
            var settings = new Dictionary<string, string>();

            if (File.Exists(settingsFilePath))
            {
                foreach (var line in File.ReadAllLines(settingsFilePath))
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        settings[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }

            return settings;
        }

        public static void SaveSettings(Dictionary<string, string> settings)
        {
            List<string> lines = new List<string>();

            foreach (var kvp in settings)
            {
                lines.Add($"{kvp.Key}={kvp.Value}");
            }

            File.WriteAllLines(settingsFilePath, lines);
        }

    }
}
