/*
** Pixelyth-Menu - Managers/DiscordRPC/Entities/ActivityType.cs
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

namespace Pixelyth.Managers.DiscordRPC.Entities
{
    /// <summary>
    /// Rich Presence activity type
    /// </summary>
    public enum ActivityType
    {
        // Streaming (1) and Custom (4) are not supported via RPC
        /// <summary>
        /// Playing status type. Displays as "Playing ..."
        /// </summary>
        Playing = 0,
        /// <summary>
        /// Listening status type. Displays as "Listening to ..."
        /// </summary>
        Listening = 2,
        /// <summary>
        /// Watching status type. Displays as "Watching ..."
        /// </summary>
        Watching = 3,
        /// <summary>
        /// Competing status type. Displays as "Competing in ..."
        /// </summary>
        Competing = 5
    }
}
