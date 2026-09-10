/*
** Pixelyth-Menu - Managers/DiscordRPC/Message/ErrorMessage.cs
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

namespace Pixelyth.Managers.DiscordRPC.Message
{
    /// <summary>
    /// Created when a error occurs within the ipc and it is sent to the client.
    /// </summary>
    public class ErrorMessage : IMessage
    {
        /// <summary>
        /// The type of message received from discord
        /// </summary>
        public override MessageType Type { get { return MessageType.Error; } }

        /// <summary>
        /// The Discord error code.
        /// </summary>
        [JsonProperty("code")]
        public ErrorCode Code { get; internal set; }

        /// <summary>
        /// The message associated with the error code.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; internal set; }

    }

    /// <summary>
    /// The error message received by discord. See https://discordapp.com/developers/docs/topics/rpc#rpc-server-payloads-rpc-errors for documentation
    /// </summary>
    public enum ErrorCode
    {
        //Pipe Error Codes
        /// <summary> Pipe was Successful </summary>
        Success = 0,

        ///<summary>The pipe had an exception</summary>
        PipeException = 1,

        ///<summary>The pipe received corrupted data</summary>
        ReadCorrupt = 2,

        //Custom Error Code
        ///<summary>The functionality was not yet implemented</summary>
        NotImplemented = 10,

        //Discord RPC error codes
        ///<summary>Unknown Discord error</summary>
        UnknownError = 1000,

        ///<summary>Invalid Payload received</summary>
        InvalidPayload = 4000,

        ///<summary>Invalid command was sent</summary>
        InvalidCommand = 4002,

        /// <summary>Invalid event was sent </summary>
        InvalidEvent = 4004,

        /*
		InvalidGuild = 4003,
		InvalidChannel = 4005,
		InvalidPermissions = 4006,
		InvalidClientID = 4007,
		InvalidOrigin = 4008,
		InvalidToken = 4009,
		InvalidUser = 4010,
		OAuth2Error = 5000,
		SelectChannelTimeout = 5001,
		GetGuildTimeout = 5002,
		SelectVoiceForceRequired = 5003,
		CaptureShortcutAlreadyListening = 5004
		*/
    }
}
