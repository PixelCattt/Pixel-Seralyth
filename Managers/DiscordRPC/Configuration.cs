/*
** Pixelyth-Menu - Managers/DiscordRPC/Configuration.cs
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

namespace Pixelyth.Managers.DiscordRPC
{
    /// <summary>
    /// Configuration of the current RPC connection
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The Discord API endpoint that should be used.
        /// </summary>
        [JsonProperty("api_endpoint")]
        public string ApiEndpoint { get; set; }

        /// <summary>
        /// The CDN endpoint
        /// </summary>
        [JsonProperty("cdn_host")]
        public string CdnHost { get; set; }

        /// <summary>
        /// The type of environment the connection on. Usually Production. 
        /// </summary>
        [JsonProperty("environment")]
        public string Environment { get; set; }
    }
}
