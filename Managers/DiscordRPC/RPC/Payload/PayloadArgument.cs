/*
** Pixelyth-Menu - Managers/DiscordRPC/RPC/Payload/PayloadArgument.cs
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
using Valve.Newtonsoft.Json.Linq;

namespace Pixelyth.Managers.DiscordRPC.RPC.Payload
{
    /// <summary>
    /// The payload that is sent by the client to discord for events such as setting the rich presence.
    /// <para>
    /// SetPrecense
    /// </para>
    /// </summary>
    internal class ArgumentPayload : IPayload
    {
        /// <summary>
        /// The data the server sent too us
        /// </summary>
        [JsonProperty("args", NullValueHandling = NullValueHandling.Ignore)]
        public JObject Arguments { get; set; }

        public ArgumentPayload() { Arguments = null; }
        public ArgumentPayload(long nonce) : base(nonce) { Arguments = null; }
        public ArgumentPayload(object args, long nonce) : base(nonce)
        {
            SetObject(args);
        }

        /// <summary>
        /// Sets the obejct stored within the data.
        /// </summary>
        /// <param name="obj"></param>
        public void SetObject(object obj)
        {
            Arguments = JObject.FromObject(obj);
        }

        /// <summary>
        /// Gets the object stored within the Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetObject<T>()
        {
            return Arguments.ToObject<T>();
        }

        public override string ToString()
        {
            return "Argument " + base.ToString();
        }
    }
}

