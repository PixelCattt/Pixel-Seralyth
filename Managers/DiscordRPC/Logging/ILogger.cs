/*
** Pixelyth-Menu - Managers/DiscordRPC/Logging/ILogger.cs
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
    /// Logging interface to log the internal states of the pipe. Logs are sent in a NON thread safe way. They can come from multiple threads and it is upto the ILogger to account for it.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// The level of logging to apply to this logger.
        /// </summary>
        LogLevel Level { get; set; }

        /// <summary>
        /// Debug trace messeages used for debugging internal elements.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Trace(string message, params object[] args);

        /// <summary>
        /// Informative log messages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Warning log messages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Warning(string message, params object[] args);

        /// <summary>
        /// Error log messsages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Error(string message, params object[] args);
    }
}
