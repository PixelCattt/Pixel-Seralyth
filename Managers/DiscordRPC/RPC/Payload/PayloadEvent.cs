/*
** Pixelyth-Menu - Managers/DiscordRPC/RPC/Payload/PayloadEvent.cs
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
using Valve.Newtonsoft.Json.Linq;

namespace Pixelyth.Managers.DiscordRPC.RPC.Payload
{
    /// <summary>
    /// Used for Discord IPC Events
    /// </summary>
    internal class EventPayload : IPayload
    {
        /// <summary>
        /// The data the server sent too us
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public JObject Data { get; set; }

        /// <summary>
        /// The type of event the server sent
        /// </summary>
        [JsonProperty("evt"), JsonConverter(typeof(EnumSnakeCaseConverter))]
        public ServerEvent? Event { get; set; }

        /// <summary>
        /// Creates a payload with empty data
        /// </summary>
		public EventPayload() : base() { Data = null; }

        /// <summary>
        /// Creates a payload with empty data and a set nonce
        /// </summary>
        /// <param name="nonce"></param>
		public EventPayload(long nonce) : base(nonce) { Data = null; }

        /// <summary>
        /// Gets the object stored within the Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetObject<T>()
        {
            return Data == null ? default(T) : Data.ToObject<T>();
        }

        /// <summary>
        /// Converts the object into a human readable string
        /// </summary>
        /// <returns></returns>
		public override string ToString()
        {
            return "Event " + base.ToString() + ", Event: " + (Event.HasValue ? Event.ToString() : "N/A");
        }
    }


}
