/*
** Pixelyth-Menu - Managers/DiscordRPC/EventType.cs
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

namespace Pixelyth.Managers.DiscordRPC
{
    /// <summary>
    /// The type of event receieved by the RPC. A flag type that can be combined.
    /// </summary>
    [System.Flags]
    public enum EventType
    {
        /// <summary>
        /// No event
        /// </summary>
        None = 0,

        /// <summary>
        /// Called when the Discord Client wishes to enter a game to spectate
        /// </summary>
        [System.Obsolete("Spectating is no longer supported by Discord.")]
        Spectate = 0x1,

        /// <summary>
        /// Called when the Discord Client wishes to enter a game to play.
        /// </summary>
        Join = 0x2,

        /// <summary>
        /// Called when another Discord Client has requested permission to join this game.
        /// </summary>
        JoinRequest = 0x4
    }
}
