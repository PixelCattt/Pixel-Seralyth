/*
** Pixelyth-Menu - Managers/DiscordRPC/Logging/LogLevel.cs
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

namespace Pixelyth.Managers.DiscordRPC.Logging
{
    /// <summary>
    /// Level of logging to use.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Trace, Info, Warning and Errors are logged
        /// </summary>
        Trace = 1,

        /// <summary>
        /// Info, Warning and Errors are logged
        /// </summary>
        Info = 2,

        /// <summary>
        /// Warning and Errors are logged
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Only Errors are logged
        /// </summary>
        Error = 4,

        /// <summary>
        /// Nothing is logged
        /// </summary>
        None = 256
    }
}
