/*
** Pixelyth-Menu - Managers/DiscordRPC/RPC/Payload/Command.cs
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
using System;

namespace Pixelyth.Managers.DiscordRPC.RPC.Payload
{
    /// <summary>
    /// The possible commands that can be sent and received by the server.
    /// </summary>
    internal enum Command
    {
        /// <summary>
        /// event dispatch
        /// </summary>
        [EnumValue("DISPATCH")]
        Dispatch,

        /// <summary>
        /// Called to set the activity
        /// </summary>
        [EnumValue("SET_ACTIVITY")]
        SetActivity,

        /// <summary>
        /// used to subscribe to an RPC event
        /// </summary>
        [EnumValue("SUBSCRIBE")]
        Subscribe,

        /// <summary>
        /// used to unsubscribe from an RPC event
        /// </summary>
        [EnumValue("UNSUBSCRIBE")]
        Unsubscribe,

        /// <summary>
        /// Used to accept join requests.
        /// </summary>
        [EnumValue("SEND_ACTIVITY_JOIN_INVITE")]
        SendActivityJoinInvite,

        /// <summary>
        /// Used to reject join requests.
        /// </summary>
        [EnumValue("CLOSE_ACTIVITY_JOIN_REQUEST")]
        CloseActivityJoinRequest,

        /// <summary>
        /// used to authorize a new client with your app
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        Authorize,

        /// <summary>
        /// used to authenticate an existing client with your app
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        Authenticate,

        /// <summary>
        /// used to retrieve guild information from the client
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        GetGuild,

        /// <summary>
        /// used to retrieve a list of guilds from the client
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        GetGuilds,

        /// <summary>
        /// used to retrieve channel information from the client
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        GetChannel,

        /// <summary>
        /// used to retrieve a list of channels for a guild from the client
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        GetChannels,


        /// <summary>
        /// used to change voice settings of users in voice channels
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        SetUserVoiceSettings,

        /// <summary>
        /// used to join or leave a voice channel, group dm, or dm
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        SelectVoiceChannel,

        /// <summary>
        /// used to get the current voice channel the client is in
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        GetSelectedVoiceChannel,

        /// <summary>
        /// used to join or leave a text channel, group dm, or dm
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        SelectTextChannel,

        /// <summary>
        /// used to retrieve the client's voice settings
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        GetVoiceSettings,

        /// <summary>
        /// used to set the client's voice settings
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        SetVoiceSettings,

        /// <summary>
        /// used to capture a keyboard shortcut entered by the user RPC Events
        /// </summary>
        [Obsolete("This value is appart of the RPC API and is not supported by this library.", true)]
        CaptureShortcut
    }
}
