/*
** Pixelyth-Menu - Managers/DiscordRPC/Message/JoinMessage.cs
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

using Pixelyth.Managers.DiscordRPC.Entities;
using Valve.Newtonsoft.Json;

namespace Pixelyth.Managers.DiscordRPC.Message
{
    /// <summary>
    /// Called when the Discord Client wishes for this process to join a game. D -> C.
    /// </summary>
    public class JoinMessage : IMessage
    {
        /// <summary>
        /// The type of message received from discord
        /// </summary>
        public override MessageType Type { get { return MessageType.Join; } }

        /// <summary>
        /// The <see cref="Secrets.JoinSecret" /> to connect with. 
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; internal set; }
    }
}
