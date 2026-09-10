/*
** Pixelyth-Menu - Managers/DiscordRPC/Message/MessageType.cs
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
    /// Type of message.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The Discord Client is ready to send and receive messages.
        /// </summary>
        Ready,

        /// <summary>
        /// The connection to the Discord Client is lost. The connection will remain close and unready to accept messages until the Ready event is called again.
        /// </summary>
        Close,

        /// <summary>
        /// A error has occured during the transmission of a message. For example, if a bad Rich Presence payload is sent, this event will be called explaining what went wrong.
        /// </summary>
        Error,

        /// <summary>
        /// The Discord Client has updated the presence.
        /// </summary>
        PresenceUpdate,

        /// <summary>
        /// The Discord Client has subscribed to an event.
        /// </summary>
        Subscribe,

        /// <summary>
        /// The Discord Client has unsubscribed from an event.
        /// </summary>
        Unsubscribe,

        /// <summary>
        /// The Discord Client wishes for this process to join a game.
        /// </summary>
        Join,

        /// <summary>
        /// The Discord Client wishes for this process to spectate a game. 
        /// </summary>
        Spectate,

        /// <summary>
        /// Another discord user requests permission to join this game.
        /// </summary>
        JoinRequest,

        /// <summary>
        /// The connection to the discord client was succesfull. This is called before <see cref="Ready"/>.
        /// </summary>
        ConnectionEstablished,

        /// <summary>
        /// Failed to establish any connection with discord. Discord is potentially not running?
        /// </summary>
        ConnectionFailed
    }
}
