/*
** Pixelyth-Menu - Managers/DiscordRPC/Exceptions/StringOutOfRangeException.cs
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

using System;

namespace Pixelyth.Managers.DiscordRPC.Exceptions
{
    /// <summary>
    /// A StringOutOfRangeException is thrown when the length of a string exceeds the allowed limit.
    /// </summary>
    public class StringOutOfRangeException : Exception
    {
        /// <summary>
        /// Maximum length the string is allowed to be.
        /// </summary>
        public int MaximumLength { get; private set; }

        /// <summary>
        /// Minimum length the string is allowed to be.
        /// </summary>
        public int MinimumLength { get; private set; }

        /// <summary>
        /// Creates a new string out of range exception with a range of min to max and a custom message
        /// </summary>
        /// <param name="message">The custom message</param>
        /// <param name="min">Minimum length the string can be</param>
        /// <param name="max">Maximum length the string can be</param>
        internal StringOutOfRangeException(string message, int min, int max) : base(message)
        {
            MinimumLength = min;
            MaximumLength = max;
        }

        /// <summary>
        /// Creates a new sting out of range exception with a range of min to max
        /// </summary>
        /// <param name="minumum"></param>
        /// <param name="max"></param>
        internal StringOutOfRangeException(int minumum, int max)
            : this($"Length of string is out of range. Expected a value between {minumum} and {max}", minumum, max) { }

        /// <summary>
        /// Creates a new sting out of range exception with a range of 0 to max
        /// </summary>
        /// <param name="max"></param>
        internal StringOutOfRangeException(int max)
            : this($"Length of string is out of range. Expected a value with a maximum length of {max}", 0, max) { }
    }
}
