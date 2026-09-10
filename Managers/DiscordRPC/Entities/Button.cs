/*
** Pixelyth-Menu - Managers/DiscordRPC/Entities/Button.cs
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

using Pixelyth.Managers.DiscordRPC.Exceptions;
using System;
using System.Text;
using Valve.Newtonsoft.Json;

namespace Pixelyth.Managers.DiscordRPC.Entities
{
    /// <summary>
    /// A Rich Presence button.
    /// </summary>
    public class Button
    {
        /// <summary>
        /// Text shown on the button
        /// <para>Max 31 bytes.</para>
        /// </summary>
        [JsonProperty("label")]
        public string Label
        {
            get { return _label; }
            set
            {
                if (!BaseRichPresence.ValidateString(value, out _label, true, 31, Encoding.UTF8))
                    throw new StringOutOfRangeException(31);
            }
        }
        private string _label;

        /// <summary>
        /// The URL opened when clicking the button.
        /// <para>Max 512 characters.</para>
        /// </summary>
        [JsonProperty("url")]
        public string Url
        {
            get { return _url; }
            set
            {
                if (!BaseRichPresence.ValidateString(value, out _url, false, 512))
                    throw new StringOutOfRangeException(512);

                if (!BaseRichPresence.ValidateUrl(_url))
                    throw new ArgumentException("Url must be a valid URI");
            }
        }
        private string _url;
    }

}
