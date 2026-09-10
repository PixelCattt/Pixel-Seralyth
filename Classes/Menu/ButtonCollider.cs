/*
** Pixelyth-Menu - Classes/Menu/ButtonCollider.cs
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

using Pixelyth.Managers;
using UnityEngine;
using static Pixelyth.Menu.Main;

namespace Pixelyth.Classes.Menu
{
    public class ButtonCollider : MonoBehaviour
    {
        public string relatedText;

        public bool incremental;
        public bool positive;

        public void OnTriggerEnter(Collider collider)
        {
            if (!(Time.time > buttonCooldown) ||
                (collider != buttonCollider && collider != lKeyCollider && collider != rKeyCollider) || joystickMenu ||
                menu == null) return;
            buttonCooldown = Time.time + 0.2f;
            if (relatedText != "Global Return") // HARDCODED GLOBAL RETURN CHECK (im gonna forget)
                SoundManager.Play(SoundManager.DefaultSounds["Button"], buttonText: relatedText);

            if (annoyingMode)
            {
                if (Random.Range(1, 5) == 2)
                {
                    NotificationManager.SendNotification("Error");
                    return;
                }
            }

            if (incremental)
                ToggleIncremental(relatedText, positive);
            else
                Toggle(relatedText, true);
        }
    }
}
