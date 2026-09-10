/*
** Pixelyth-Menu - Patches/Menu/OwnershipPatch.cs
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
using Photon.Pun;
using System.Collections.Generic;

namespace Pixelyth.Patches.Menu
{
    [HarmonyPatch(typeof(RequestableOwnershipGuard), nameof(RequestableOwnershipGuard.OwnershipRequested))]
    public class OwnershipPatch
    {
        public static bool enabled;
        public static readonly List<RequestableOwnershipGuard> blacklistedGuards = new List<RequestableOwnershipGuard>();

        public static bool Prefix(RequestableOwnershipGuard __instance, string nonce, PhotonMessageInfo info) =>
            !enabled || (__instance.photonView.IsMine && !blacklistedGuards.Contains(__instance));
    }
}
