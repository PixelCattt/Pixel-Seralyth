/*
** Pixelyth-Menu - Managers/CoroutineManager.cs
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

using System.Collections;
using UnityEngine;

namespace Pixelyth.Managers
{
    public class CoroutineManager : MonoBehaviour
    {
        public static CoroutineManager instance;

        private void Awake() =>
            instance = this;

        [System.Obsolete("RunCoroutine is obsolete. Use StartCoroutine directly on MonoBehaviour instances instead.")]
        public static Coroutine RunCoroutine(IEnumerator enumerator) =>
            instance.StartCoroutine(enumerator);

        [System.Obsolete("EndCoroutine is obsolete. Use StopCoroutine directly on MonoBehaviour instances instead.")]
        public static void EndCoroutine(Coroutine enumerator) =>
            instance.StopCoroutine(enumerator);
    }
}
