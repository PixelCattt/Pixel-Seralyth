/*
** Pixelyth-Menu - Managers/DiscordRPC/RPC/Payload/IPayload.cs
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

using Pixelyth.Managers.DiscordRPC.Converters;
using Valve.Newtonsoft.Json;

namespace Pixelyth.Managers.DiscordRPC.RPC.Payload
{
    /// <summary>
    /// Base Payload that is received by both client and server
    /// </summary>
    internal abstract class IPayload
    {
        /// <summary>
        /// The type of payload
        /// </summary>
        [JsonProperty("cmd"), JsonConverter(typeof(EnumSnakeCaseConverter))]
        public Command Command { get; set; }

        /// <summary>
        /// A incremental value to help identify payloads
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        protected IPayload() { }
        protected IPayload(long nonce)
        {
            Nonce = nonce.ToString();
        }

        public override string ToString()
        {
            return $"Payload || Command: {Command}, Nonce: {Nonce}";
        }
    }
}

