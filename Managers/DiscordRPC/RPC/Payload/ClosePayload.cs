/*
** Pixelyth-Menu - Managers/DiscordRPC/RPC/Payload/ClosePayload.cs
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

using Valve.Newtonsoft.Json;

namespace Pixelyth.Managers.DiscordRPC.RPC.Payload
{
    internal class ClosePayload : IPayload
    {
        /// <summary>
        /// The close code the discord gave us
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// The close reason discord gave us
        /// </summary>
        [JsonProperty("message")]
        public string Reason { get; set; }

        [JsonConstructor]
        public ClosePayload()
            : base()
        {
            Code = -1;
            Reason = "";
        }
    }
}
