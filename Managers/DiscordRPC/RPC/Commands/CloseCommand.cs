/*
** Pixelyth-Menu - Managers/DiscordRPC/RPC/Commands/CloseCommand.cs
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

using Pixelyth.Managers.DiscordRPC.RPC.Payload;
using Valve.Newtonsoft.Json;

namespace Pixelyth.Managers.DiscordRPC.RPC.Commands
{
    internal class CloseCommand : ICommand
    {
        /// <summary>
        /// The process ID
        /// </summary>
        [JsonProperty("pid")]
        public int PID { get; set; }

        /// <summary>
        /// The rich presence to be set. Can be null.
        /// </summary>
        [JsonProperty("close_reason")]
        public string value = "Unity 5.5 doesn't handle thread aborts. Can you please close me discord?";

        public IPayload PreparePayload(long nonce)
        {
            return new ArgumentPayload()
            {
                Command = Command.Dispatch,
                Nonce = null,
                Arguments = null
            };
        }
    }
}
