/*
** Pixelyth-Menu - Managers/DiscordRPC/Message/ConnectionEstablishedMessage.cs
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

namespace Pixelyth.Managers.DiscordRPC.Message
{
    /// <summary>
    /// The connection to the discord client was succesfull. This is called before <see cref="MessageType.Ready"/>.
    /// </summary>
    public class ConnectionEstablishedMessage : IMessage
    {
        /// <summary>
        /// The type of message received from discord
        /// </summary>
        public override MessageType Type { get { return MessageType.ConnectionEstablished; } }

        /// <summary>
        /// The pipe we ended up connecting too
        /// </summary>
        [System.Obsolete("The connected pipe is not neccessary information.")]
        public int ConnectedPipe { get; internal set; }
    }
}
