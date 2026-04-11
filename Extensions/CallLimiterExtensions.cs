/*
 * Seralyth Menu  Extensions/CallLimiterExtensions.cs
 * A community driven mod menu for Gorilla Tag with over 1000+ mods
 *
 * Copyright (C) 2026  Seralyth Software
 * https://github.com/Seralyth/Seralyth-Menu
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using UnityEngine;

namespace Seralyth.Extensions
{
    public static class CallLimiterExtensions
    {
        public static float GetTimeDelay(this CallLimiter limiter, float time)
        {
            if (limiter == null || limiter.callTimeHistory == null || limiter.callHistoryLength <= 0)
                return 0f;

            int index = Mathf.Clamp(limiter.oldTimeIndex, 0, limiter.callHistoryLength - 1);
            float readyAt = limiter.callTimeHistory[index];

            if (readyAt == float.MinValue)
                return 0f;

            return Mathf.Max(0f, readyAt - time);
        }

        public static bool CanCallNow(this CallLimiter limiter, float time)
        {
            return GetTimeDelay(limiter, time) <= 0f;
        }
    }
}
