/*
** Pixelyth-Menu - Managers/DiscordRPC/IO/Opcode.cs
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

namespace Pixelyth.Managers.DiscordRPC.IO
{
    /// <summary>
    /// The operation code that the <see cref="PipeFrame"/> was sent under. This defines the type of frame and the data to expect.
    /// </summary>
    public enum Opcode : uint
    {
        /// <summary>
        /// Initial handshake frame
        /// </summary>
        Handshake = 0,

        /// <summary>
        /// Generic message frame
        /// </summary>
        Frame = 1,

        /// <summary>
        /// Discord has closed the connection
        /// </summary>
        Close = 2,

        /// <summary>
        /// Ping frame (not used?)
        /// </summary>
        Ping = 3,

        /// <summary>
        /// Pong frame (not used?)
        /// </summary>
        Pong = 4
    }
}
