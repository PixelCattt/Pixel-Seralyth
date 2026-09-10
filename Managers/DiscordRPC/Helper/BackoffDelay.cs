/*
** Pixelyth-Menu - Managers/DiscordRPC/Helper/BackoffDelay.cs
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

namespace Pixelyth.Managers.DiscordRPC.Helper
{

    internal class BackoffDelay
    {
        /// <summary>
        /// The maximum time the backoff can reach
        /// </summary>
        public int Maximum { get; private set; }

        /// <summary>
        /// The minimum time the backoff can start at
        /// </summary>
        public int Minimum { get; private set; }

        /// <summary>
        /// The current time of the backoff
        /// </summary>
        public int Current { get { return _current; } }
        private int _current;

        /// <summary>
        /// The current number of failures
        /// </summary>
        public int Fails { get { return _fails; } }
        private int _fails;

        /// <summary>
        /// The random generator
        /// </summary>
        public Random Random { get; set; }

        private BackoffDelay() { }
        public BackoffDelay(int min, int max) : this(min, max, new Random()) { }
        public BackoffDelay(int min, int max, Random random)
        {
            Minimum = min;
            Maximum = max;

            _current = min;
            _fails = 0;
            Random = random;
        }

        /// <summary>
        /// Resets the backoff
        /// </summary>
        public void Reset()
        {
            _fails = 0;
            _current = Minimum;
        }

        public int NextDelay()
        {
            //Increment the failures
            _fails++;

            double diff = (Maximum - Minimum) / 100f;
            _current = (int)Math.Floor(diff * _fails) + Minimum;


            return Math.Min(Math.Max(_current, Minimum), Maximum);
        }
    }
}
