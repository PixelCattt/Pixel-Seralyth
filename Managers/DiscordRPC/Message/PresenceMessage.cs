/*
** Pixelyth-Menu - Managers/DiscordRPC/Message/PresenceMessage.cs
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

namespace Pixelyth.Managers.DiscordRPC.Message
{
    /// <summary>
    /// Representation of the message received by discord when the presence has been updated.
    /// </summary>
    public class PresenceMessage : IMessage
    {
        /// <summary>
        /// The type of message received from discord
        /// </summary>
        public override MessageType Type { get { return MessageType.PresenceUpdate; } }

        internal PresenceMessage() : this(null) { }
        internal PresenceMessage(RichPresenceResponse rpr)
        {
            if (rpr == null)
            {
                Presence = null;
                Name = "No Rich Presence";
                ApplicationID = "";
            }
            else
            {
                Presence = rpr;
                Name = rpr.Name;
                ApplicationID = rpr.ClientID;
            }
        }

        /// <summary>
        /// The rich presence Discord has set
        /// </summary>
        public BaseRichPresence Presence { get; internal set; }

        /// <summary>
        /// The name of the application Discord has set it for
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The ID of the application discord has set it for
        /// </summary>
        public string ApplicationID { get; internal set; }
    }
}
