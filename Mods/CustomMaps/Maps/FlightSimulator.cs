/*
** Pixelyth-Menu - Mods/CustomMaps/Maps/FlightSimulator.cs
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

using Pixelyth.Classes.Menu;
using System.Collections.Generic;
using static Pixelyth.Mods.CustomMaps.Manager;

namespace Pixelyth.Mods.CustomMaps.Maps
{
    public class FlightSimulator : CustomMap
    {
        public override long MapID => 5024157;
        public override ButtonInfo[] Buttons => new[]
        {
            new ButtonInfo { buttonText = "Steal Pilot", enableMethod = StealPilot, disableMethod = DisableStealPilot, toolTip = "Allows you to steal the pilot position from other people's planes."},
        };

        public static void StealPilot()
        {
            ModifyCustomScript(new Dictionary<int, string>
                    {
                        { 373, "if isButtonPressed(pilotClaimButtonJet) then" }
                    });
        }
        public static void DisableStealPilot() =>
            RevertCustomScript(373);
    }
}
