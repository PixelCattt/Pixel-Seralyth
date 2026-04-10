/*
 * Seralyth Menu  Managers/AdminPermissionManager.cs
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

using Seralyth.Classes.Menu;
using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using Seralyth.Menu;
using System;
using System.Threading.Tasks;

namespace Seralyth.Managers
{
    public static class AdminPermissionManager
    {
        public static HashSet<string> allowedCommandList = new HashSet<string>();

        public static void AddCommandToList(string command)
        {
            if (!allowedCommandList.Contains(command))
                allowedCommandList.Add(command);

            Buttons.GetIndex(command).toolTip = "Removes the " + Buttons.GetIndex(command).overlapText + " Admin-Command from the List of Allowed Commands.";
        }

        public static void RemoveCommandFromList(string command)
        {
            if (allowedCommandList.Contains(command))
                allowedCommandList.Remove(command);

            Buttons.GetIndex(command).toolTip = "Adds the " + Buttons.GetIndex(command).overlapText + " Admin-Command to the List of Allowed Commands.";
        }

        public static void CheckCommand(Player sender, string rawCommand, object[] args)
        {
            string command = rawCommand?.Trim().ToLower() ?? "";

            int adminType = 2;

            if (ServerData.Administrators.TryGetValue(sender.UserId, out var administrator))
            {
                adminType = 1;

                if (ServerData.SuperAdministrators.Contains(administrator))
                    adminType = 2;
            }

            bool allowed = allowedCommandList.Contains(command);

            bool executed = allowed && adminType != 0;

            if (executed)
            {
                Classes.Menu.Console.HandleConsoleEvent(sender, command, args);
            }

            NotifyCommand(sender, command, args, executed, adminType);
        }

        private static void NotifyCommand(Player sender, string command, object[] args, bool allowed, int adminType)
        {
            string stateColor = allowed ? "green" : "red";
            string stateText = allowed ? "EXECUTED" : "BLOCKED";

            string argsString = (args != null && args.Length > 1)
                ? string.Join(", ", args.Skip(1))
                : "";

            string adminTypeText = adminType == 0
                ? "<color=red>NON-ADMIN</color>"
                : adminType == 1
                    ? "<color=yellow>ADMIN</color>"
                    : "<color=purple>SUPER</color>";

            string message =
                "<color=grey>[</color>" +
                adminTypeText +
                "<color=grey>]</color> " +

                "<color=grey>[</color>" +
                sender.NickName +
                "<color=grey>]</color> " +

                "<color=grey>(</color>" +
                $"<color={stateColor}>{stateText}</color>" +
                "<color=grey>)</color> " +

                $"{command} {argsString}";

            NotificationManager.SendNotification(message, 7500);
        }
    }
}