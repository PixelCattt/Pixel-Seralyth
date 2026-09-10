/*
** Pixelyth-Menu - Bootstrapper.cs
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

using Pixelyth.Managers;
using Pixelyth.Menu;
using Pixelyth.Patches;
using Pixelyth.Patches.Menu;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Pixelyth
{
    internal static class Bootstrapper
    {
        private static bool initialized;
        public static bool FirstLaunch;
        public static GameObject Loader;

        private const float IntegrityCheckIntervalSeconds = 120f;

        internal static void Initialize()
        {
            if (initialized) return;
            initialized = true;
            FirstLaunch = !Directory.Exists(PluginInfo.BaseDirectory);
            string[] existingDirectories =
            {
                "",
                "/Sounds",
                "/Plugins",
                "/Backups",
                "/Macros",
                "/TTS",
                "/PlayerInfo",
                "/CustomScripts",
                "/Friends",
                "/Friends/Messages",
                "/Achievements"
            };
            foreach (string dir in existingDirectories)
            {
                string target = $"{PluginInfo.BaseDirectory}{dir}";
                if (!Directory.Exists(target))
                    Directory.CreateDirectory(target);
            }
            PatchHandler.PatchAll(true);
            if (File.Exists($"{PluginInfo.BaseDirectory}/Pixelyth_Preferences.txt"))
            {
                if (File.ReadAllLines($"{PluginInfo.BaseDirectory}/Pixelyth_Preferences.txt")[0]
                    .Split(";;")
                    .Contains("Accept TOS"))
                {
                    TOSPatches.enabled = true;
                }
            }

            GorillaTagger.OnPlayerSpawned(LoadMenu);
        }
        private static void LoadMenu()
        {
            PatchHandler.PatchAll();
            Loader = new GameObject("Pixelyth_Loader");
            CoroutineManager coroutineManager = Loader.AddComponent<CoroutineManager>();
            Loader.AddComponent<NotificationManager>();
            Loader.AddComponent<CustomBoardManager>();
            Loader.AddComponent<UI>();
            UnityEngine.Object.DontDestroyOnLoad(Loader);
            coroutineManager.StartCoroutine(PatchIntegrityLoop());
        }

        private static IEnumerator PatchIntegrityLoop()
        {
            var wait = new WaitForSeconds(IntegrityCheckIntervalSeconds);

            while (true)
            {
                if (PatchHandler.instance != null)
                {
                    try
                    {
                        PatchHandler.PatchIntegrityCheck();
                    }
                    catch (Exception ex)
                    {
                        LogManager.LogError($"PatchIntegrityCheck threw: {ex}");
                    }
                }

                yield return wait;
            }
        }
    }
}