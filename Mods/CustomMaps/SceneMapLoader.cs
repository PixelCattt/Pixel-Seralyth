/*
** Pixelyth-Menu - Mods/CustomMaps/SceneMapLoader.cs
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

using UnityEngine.SceneManagement;

namespace Pixelyth.Mods.CustomMaps
{
    public static class SceneMapLoader
    {
        public static void Init()
        {
            SceneMapRegistry.FillRegistry();
            SceneManager.activeSceneChanged += OnSceneChanged;
            CheckSceneForMap(SceneManager.GetActiveScene().name);
        }

        public static void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            CheckSceneForMap(newScene.name);
        }

        public static void CheckSceneForMap(string sceneName)
        {
            var map = SceneMapRegistry.GetMapForScene(sceneName);
            if (map != null)
                Manager.UpdateCustomMapsTab(map.MapID);
            else
                Manager.UpdateCustomMapsTab();
        }
    }
}
