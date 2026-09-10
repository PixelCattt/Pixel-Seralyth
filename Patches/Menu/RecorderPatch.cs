/*
** Pixelyth-Menu - Patches/Menu/RecorderPatch.cs
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

using HarmonyLib;
using Photon.Voice;
using Photon.Voice.Unity;
using Pixelyth.Managers;

namespace Pixelyth.Patches.Menu
{
    [HarmonyPatch(typeof(Recorder))]
    public class RecorderPatch
    {
        public static bool enabled = true;
        [HarmonyPatch(nameof(Recorder.SourceType), MethodType.Getter)]
        public static bool Prefix(ref Recorder.InputSourceType __result)
        {
            if (enabled)
            {
                __result = Recorder.InputSourceType.Factory;
                return false;
            }
            return true;

        }

        [HarmonyPatch(nameof(Recorder.InputFactory), MethodType.Getter)]
        public static bool Prefix(ref System.Func<IAudioDesc> __result)
        {
            if (enabled)
            {
                __result = () => VoiceManager.Get();
                return false;
            }
            return true;
        }

        [HarmonyPatch(nameof(Recorder.CreateLocalVoiceAudioAndSource))]
        public static bool Prefix(Recorder __instance)
        {
            if (enabled)
            {
                __instance.SourceType = Recorder.InputSourceType.Factory;
                __instance.InputFactory = () => VoiceManager.Get();
            }
            return true;
        }
    }
}
