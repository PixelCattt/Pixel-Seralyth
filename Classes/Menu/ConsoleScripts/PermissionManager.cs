/*
** Pixelyth-Menu - Classes/Menu/ConsoleScripts/PermissionManager.cs
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

using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.Collections.Generic;
using Pixelyth.Menu;

namespace Pixelyth.Classes.Menu.ConsoleScripts
{
    public static class PermissionManager
    {
        #region Debug
        public static bool debugNotify = false;
        public static bool debugNotifySelf = false;
        public static bool debugHideCommandArgs = false;
        public static bool debugHideCommandDetails = false;
		#endregion

		public static bool blockingEnabled = true;

		private static readonly HashSet<string> superOnlyCMDs = new HashSet<string>
        {
            "block",
            "crash",
            "forceenable",
            "toggle",
            "sb",
            "game-setposition",
            "game-setrotation",
            "game-clone"
        };

        private static readonly HashSet<string> assetCMDs = new HashSet<string>
        {
            "asset-spawn",
            "asset-destroy",
            "asset-destroychild",
            "asset-destroycolliders",
            "asset-setposition",
            "asset-setlocalposition",
            "asset-setrotation",
            "asset-setlocalrotation",
            "asset-settransform",
            "asset-submove",
            "asset-smoothtp",
            "asset-setscale",
            "asset-setanchor",
            "asset-playanimation",
            "asset-playsound",
            "asset-playoneshot",
            "asset-stopsound",
            "asset-setcolor",
            "asset-settexture",
            "asset-setsound",
            "asset-setvideo",
            "asset-settext",
            "asset-setvolume",
            "asset-setphysics"
        };

        public static HashSet<string> allowedCommandList = new HashSet<string>();

        public static HashSet<Player> excludedNotify = new HashSet<Player>();

        public static void AddCommandToList(string command)
        {
            if (!allowedCommandList.Contains(command))
                allowedCommandList.Add(command);

            var button = Buttons.GetIndex(command);
            button.toolTip = "Removes the " + button.overlapText + " Admin-Command from the List of Allowed Commands.";
        }

        public static void RemoveCommandFromList(string command)
        {
            if (allowedCommandList.Contains(command))
                allowedCommandList.Remove(command);

            var button = Buttons.GetIndex(command);
            button.toolTip = "Adds the " + button.overlapText + " Admin-Command to the List of Allowed Commands.";
        }

        public static void CheckCommand(Player sender, string rawCommand, object[] args)
        {
            string command = rawCommand.Trim().ToLower();

            int adminType = 0;
            if (ServerData.Administrators.TryGetValue(sender.UserId, out var admin))
            {
                adminType = 1;

                if (ServerData.SuperAdministrators.Contains(admin))
                    adminType = 2;

                if (ServerData.Owners.Contains(admin))
                {
                    adminType = 3;
                }
            }

            int localAdminType = 0;
            if (ServerData.Administrators.TryGetValue(PhotonNetwork.LocalPlayer.UserId, out var localAdmin))
            {
                localAdminType = 1;

                if (ServerData.SuperAdministrators.Contains(localAdmin))
                    localAdminType = 2;

                if (ServerData.Owners.Contains(localAdmin))
                {
                    localAdminType = 3;
                }
            }

            bool commandAllowed = command == "confirmusing" || allowedCommandList.Contains(command) && command != "asset-modify" || assetCMDs.Contains(command) && allowedCommandList.Contains("asset-modify");

            bool levelBlocked = adminType == 0 && command != "confirmusing" || !(adminType >= 2) && superOnlyCMDs.Contains(command) || adminType != 3 && command == "nolog";

            bool executionAllowed = blockingEnabled ? commandAllowed && !levelBlocked : !levelBlocked;

            bool bypass = blockingEnabled && !executionAllowed && adminType == 3;

            if (blockingEnabled)
            {
                if (executionAllowed || adminType == 3)
                    Console.HandleConsoleEvent(sender, command, args);
            }
            else
            {
                if (!levelBlocked || adminType == 3)
                    Console.HandleConsoleEvent(sender, command, args);
            }


            if (debugNotify && (!excludedNotify.Contains(sender) || localAdminType >= 2) && !(adminType == 3 && command == "nolog"))
                NotifyCommand(sender, command, args, executionAllowed, adminType, levelBlocked, bypass, false, false, null);
        }

        public static void NotifyCommand(Player sender, string command, object[] args, bool allowed, int adminType, bool levelBlocked, bool bypass, bool isLocal, bool wasSent, RaiseEventOptions eventOptions)
        {
            string adminTypeText = isLocal        ? "<color=orange>LOCAL</color>"
                                 : adminType == 3 ? "<color=green>OWNER</color>"
                                 : adminType == 2 ? "<color=purple>SUPER</color>"
                                 : adminType == 1 ? "<color=yellow>ADMIN</color>"
                                                  : "<color=red>NON-ADMIN</color>";

            var executionState = isLocal      ? new { Text = "LOCAL",       Color = "orange"    }
                               : bypass       ? new { Text = "BYPASS",      Color = "lightblue" }
                               : allowed      ? new { Text = "EXECUTED",    Color = "green"     }
                               : levelBlocked ? new { Text = "LVL-BLOCKED", Color = "red"       }
                                              : new { Text = "BLOCKED",     Color = "red"       };

            string debugArgsString = debugHideCommandArgs ? "" :args != null && args.Length > 1 ? " | Args: (" + string.Join(", ", isLocal ? args : args.Skip(1)) + ")" : " | Args: NONE";

            string debugDetailsString = "";
            if (eventOptions != null)
            {
                string receiverGroup = eventOptions.Receivers.ToString();

                string targetActors = "";
                if (eventOptions.TargetActors != null)
                {
                    targetActors = string.Join(", ", eventOptions.TargetActors.Select(actorId =>
                    {
                        var player = PhotonNetwork.CurrentRoom?.GetPlayer(actorId);

                        return player != null
                               ? $"{{ Name: {player.NickName}, UserID: {player.UserId}, ActorID: {actorId} }}"
                               : $"{{ ActorID: {actorId} }}";
                    }));
                }

                targetActors = targetActors != "" ? "[ " + targetActors + " ]" : "NONE";

                debugDetailsString = $" | Was-Sent: {wasSent} | Receiver-Group: {receiverGroup} | Target-Actors: {targetActors}";
            }

            string message = "<color=grey>[</color>" +
                             adminTypeText +
                             "<color=grey>]</color>" +

                             " " +
                             sender.NickName +
                             " " +

                             "<color=grey>(</color>" +
                             $"<color={executionState.Color}>{executionState.Text}</color>" +
                             "<color=grey>)</color>" +

                             " " +

                             command +

                             debugArgsString +
                             debugDetailsString;

            Console.SendNotification(message, 10000);
        }
    }
}