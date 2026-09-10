/*
** Pixelyth-Menu - Managers/DiscordRPC/IO/PipeLocation.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pixelyth.Managers.DiscordRPC.IO
{
    /// <summary>
    /// Provides the location of the Discord IPC pipes for the current operating system.
    /// </summary>
    public static class PipeLocation
    {
        const string DiscordPipePrefix = @"discord-ipc-";
        const int MaximumPipeVariations = 10;

        static readonly string[] LinuxPackageManagers = {
            "app/com.discordapp.Discord/",	// Flatpak
			"snap.discord/",				// Snap
			// TODO: Add more package managers as needed such as AppImage
		};

        /// <summary>
        /// Generates a sequence of pipe names that Discord can use for the IPC for the current operating system.
        /// </summary>
        /// <param name="startPipe">The starting index for the pipe names.</param>
        /// <returns>An enumerable collection of pipe names.</returns>
        /// <remarks>
        /// The names are ordered and will check multiple locations for a suitable pipe. For Unix systems, multiple TEMP directories are checked and multiple package managers are considered.
        /// This means that there is not a 1 to 1 mapping of pipe names to indices, as there are many locations for a single pipe index.
        /// </remarks>	
        public static IEnumerable<string> GetPipes(int startPipe = 0)
        {
            IsOSUnix();
            return IsOSUnix()
                ? Enumerable.Range(startPipe, MaximumPipeVariations).SelectMany(GetUnixPipes)
                : Enumerable.Range(startPipe, MaximumPipeVariations).SelectMany(GetWindowsPipes);
        }

        private static IEnumerable<string> GetWindowsPipes(int index)
        {
            yield return $"{DiscordPipePrefix}{index}";
        }

        private static IEnumerable<string> GetUnixPipes(int index)
        {
            foreach (var tempDir in TemporaryDirectories())
            {
                // No Package Manager. First because its common for MacOS. 
                // Linux users tend to either just use a browser or a package manager.
                yield return Path.Combine(tempDir, $"{DiscordPipePrefix}{index}");

                // Package Managers
                foreach (var pmDir in LinuxPackageManagers)
                    yield return Path.Combine(tempDir, pmDir, $"{DiscordPipePrefix}{index}");
            }
        }

        private static IEnumerable<string> TemporaryDirectories()
        {
            string temp;
            temp = Environment.GetEnvironmentVariable("XDG_RUNTIME_DIR");
            if (temp != null)
                yield return temp;

            temp = Environment.GetEnvironmentVariable("TMPDIR");
            if (temp != null)
                yield return temp;

            temp = Environment.GetEnvironmentVariable("TMP");
            if (temp != null)
                yield return temp;

            temp = Environment.GetEnvironmentVariable("TEMP");
            if (temp != null)
                yield return temp;

            yield return "/temp";
        }

        private static bool IsOSUnix()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                return true;

#if NETFRAMEWORK // MacOS was replaced with Unix in .NET Core
			if (Environment.OSVersion.Platform == PlatformID.MacOSX)
				return true;
#endif

            return false;
        }
    }
}
