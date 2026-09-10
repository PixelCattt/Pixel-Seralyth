/*
** Pixelyth-Menu - Patches/Menu/SpeakerPatch.cs
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
using System.Collections.Generic;

namespace Pixelyth.Patches.Menu
{
    [HarmonyPatch(typeof(Speaker), nameof(Speaker.OnAudioFrame))]
    public class SpeakerPatch
    {
        public static bool enabled;
        public static Speaker targetSpeaker;
        public static List<float> SampleQueue = new List<float>();
        public static readonly object locked = new object();

        static void Postfix(Speaker __instance, FrameOut<float> frame)
        {
            if (!enabled || targetSpeaker == null || __instance != targetSpeaker)
                return;

            var src = frame.Buf;
            if (src == null || src.Length == 0) return;

            lock (locked)
            {
                SampleQueue.AddRange(src);
                if (SampleQueue.Count > targetSpeaker.RemoteVoiceLink.Info.SamplingRate)
                    SampleQueue.RemoveRange(0, SampleQueue.Count - targetSpeaker.RemoteVoiceLink.Info.SamplingRate);
            }
        }
    }

}
