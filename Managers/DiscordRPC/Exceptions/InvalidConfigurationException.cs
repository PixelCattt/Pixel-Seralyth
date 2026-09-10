/*
** Pixelyth-Menu - Managers/DiscordRPC/Exceptions/InvalidConfigurationException.cs
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
    /// A InvalidConfigurationException is thrown when trying to perform a action that conflicts with the current configuration.
    /// </summary>
    public class InvalidConfigurationException : Exception
    {
        internal InvalidConfigurationException(string message) : base(message) { }
    }
}
