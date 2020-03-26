using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring
{
    public class RemoteController
    {
        private readonly Dictionary<string, Func<string>> commands;
        private readonly int settingsChangeStep;
        private bool isSwitchedOn;
        private int volume;
        private int contrast;
        private int brightness;

        public RemoteController()
        {
            isSwitchedOn = false;
            volume = 20;
            contrast = 20;
            brightness = 20;
            settingsChangeStep = 1;
            commands = new Dictionary<string, Func<string>>
            {
                {"Tv On", SwitchOn},
                {"Tv Off", SwitchOff},
                {"Volume Up", VolumeUp},
                {"Volume Down", VolumeDown},
                {"Options show", GetOptions},
                {"Options change brightness up", BrightnessUp},
                {"Options change brightness down", BrightnessDown},
                {"Options change contrast up", ContrastUp},
                {"Options change contrast down", ContrastDown}
            };
        }

        public string Call(string command)
        {
            if (commands.ContainsKey(command))
            {
                return commands[command].Invoke();
            }

            throw new ArgumentException("Wrong command", command);
        }

        public string GetOptions()
        {
            var options = new StringBuilder();
            options.AppendLine("Options:");
            options.AppendLine($"Volume {volume}");
            options.AppendLine($"IsOnline {isSwitchedOn}");
            options.AppendLine($"Brightness {brightness}");
            options.AppendLine($"Contrast {contrast}");
            return options.ToString();
        }

        public string SwitchOn()
        {
            isSwitchedOn = true;
            return null;
        }

        public string SwitchOff()
        {
            isSwitchedOn = false;
            return null;
        }

        public string VolumeUp()
        {
            volume = ChangeOption(volume, settingsChangeStep);
            return null;
        }

        public string VolumeDown()
        {
            volume = ChangeOption(volume, -settingsChangeStep);
            return null;
        }

        public string ContrastUp()
        {
            contrast = ChangeOption(contrast, settingsChangeStep);
            return null;
        }

        public string ContrastDown()
        {
            contrast = ChangeOption(contrast, -settingsChangeStep);
            return null;
        }

        public string BrightnessUp()
        {
            brightness = ChangeOption(brightness, settingsChangeStep);
            return null;
        }

        public string BrightnessDown()
        {
            brightness = ChangeOption(brightness, -settingsChangeStep);
            return null;
        }

        private int ChangeOption(int currentValue, int delta)
        {
            currentValue += delta;
            return currentValue < 0 ? 0 : currentValue % 101;
        }
    }
}