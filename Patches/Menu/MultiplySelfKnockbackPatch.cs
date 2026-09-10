/*
** Pixelyth-Menu - Patches/Menu/MultiplySelfKnockbackPatch.cs
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
using Pixelyth.Mods;

namespace Pixelyth.Patches.Menu
{
    [HarmonyPatch(typeof(SlingshotProjectile), nameof(SlingshotProjectile.CheckForAOEKnockback))]
    public class MultiplySelfKnockbackPatch
    {
        public static bool enabled;

        public static void Prefix(SlingshotProjectile __instance)
        {
            if (enabled && __instance.projectileOwner == NetworkSystem.Instance.LocalPlayer)
            {
                if (__instance.aoeKnockbackConfig != null)
                {
                    var config = __instance.aoeKnockbackConfig.Value;
                    config.knockbackVelocity = config.knockbackVelocity * Movement.multiplicationAmount / 10;
                    __instance.aoeKnockbackConfig = config;
                }
            }
        }
    }
}
