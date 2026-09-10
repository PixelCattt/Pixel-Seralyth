/*
** Pixelyth-Menu - Managers/DiscordRPC/Logging/DiscordLogManager.cs
** An Open-Source Mod Menu for Gorilla Tag with 2000+ Mods!
**
** Copyright (C) 2026 - PixelCatt
** https://github.com/PixelCattt/Pixelyth-Menu
**
** This program is free software: you can redistribute it and/or modify
** it under the terms of the GNU General Public License as published by
** the Free Software Foundation, either version 3 of the License, or
** (at your option) any later version.
**
** This program is distributed in the hope that it will be useful,
** but WITHOUT ANY WARRANTY; without even the implied warranty of
** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
** See the GNU General Public License for more details.
**
** You should have received a copy of the GNU General Public License
** along with this program.
** If not, see <https://www.gnu.org/licenses/>.
*/

using System;

namespace Pixelyth.Managers.DiscordRPC.Logging
{
    public class DiscordLogManager : ILogger
    {
        public LogLevel Level { get; set; }

        public bool Coloured { get; set; }

        [Obsolete("Use Coloured")]
        public bool Colored
        {
            get
            {
                return Coloured;
            }
            set
            {
                Coloured = value;
            }
        }

        public DiscordLogManager()
        {
            Level = LogLevel.Info;
            Coloured = false;
        }

        public DiscordLogManager(LogLevel level) : this()
        {
            Level = level;
        }

        public DiscordLogManager(LogLevel level, bool coloured)
        {
            Level = level;
            Coloured = coloured;
        }

        public void Trace(string message, params object[] args)
        {
            if (Level > LogLevel.Trace)
            {
                return;
            }
            if (Coloured)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            string text = "TRACE: " + message;
            if (args.Length != 0)
            {
                LogManager.Log(text, args);
                return;
            }
            LogManager.Log(text);
        }

        public void Info(string message, params object[] args)
        {
            if (Level > LogLevel.Info)
            {
                return;
            }
            string text = "INFO: " + message;
            if (args.Length != 0)
            {
                LogManager.Log(text, args);
                return;
            }
            LogManager.Log(text);
        }

        public void Warning(string message, params object[] args)
        {
            if (Level > LogLevel.Warning)
            {
                return;
            }
            string text = "WARN: " + message;
            if (args.Length != 0)
            {
                LogManager.LogWarning(text, args);
                return;
            }
            LogManager.LogWarning(text);
        }

        public void Error(string message, params object[] args)
        {
            if (Level > LogLevel.Error)
            {
                return;
            }
            string text = "ERR : " + message;
            if (args.Length != 0)
            {
                LogManager.LogError(text, args);
                return;
            }
            LogManager.LogError(text);
        }
    }
}
