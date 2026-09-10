/*
** Pixelyth-Menu - Managers/DiscordRPC/Converters/EnumSnakeCaseConverter.cs
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
using System.Linq;
using System.Reflection;
using Valve.Newtonsoft.Json;

namespace Pixelyth.Managers.DiscordRPC.Converters
{
    /// <summary>
    /// Converts enums with the <see cref="EnumValueAttribute"/> into Json friendly terms. 
    /// </summary>
    internal class EnumSnakeCaseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            object val;
            return TryParseEnum(objectType, (string)reader.Value, out val) ? val : existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var enumtype = value.GetType();
            var name = Enum.GetName(enumtype, value);

            //Get each member and look for hte correct one
            var members = enumtype.GetMembers(BindingFlags.Public | BindingFlags.Static);
            foreach (var m in members)
            {
                if (m.Name.Equals(name))
                {
                    var attributes = m.GetCustomAttributes(typeof(EnumValueAttribute), true);
                    if (attributes.Length > 0)
                    {
                        name = ((EnumValueAttribute)attributes[0]).Value;
                    }
                }
            }

            writer.WriteValue(name);
        }


        public bool TryParseEnum(Type enumType, string str, out object obj)
        {
            //Make sure the string isn;t null
            if (str == null)
            {
                obj = null;
                return false;
            }

            //Get the real type
            Type type = enumType;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = type.GetGenericArguments().First();

            //Make sure its actually a enum
            if (!type.IsEnum)
            {
                obj = null;
                return false;
            }


            //Get each member and look for hte correct one
            var members = type.GetMembers(BindingFlags.Public | BindingFlags.Static);
            foreach (var m in members)
            {
                var attributes = m.GetCustomAttributes(typeof(EnumValueAttribute), true);
                if (!attributes.Cast<EnumValueAttribute>().Any(enumValue => str.Equals(enumValue.Value))) continue;
                obj = Enum.Parse(type, m.Name, ignoreCase: true);

                return true;
            }

            //We failed
            obj = null;
            return false;
        }

    }
}
