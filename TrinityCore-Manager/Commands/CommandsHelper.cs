// TrinityCore-Manager
// Copyright (C) 2013 Mitchell Kutchuk
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Text;
using TrinityCore_Manager.Extensions;

namespace TrinityCore_Manager.Commands
{
    public static class CommandsHelper
    {

        public static string BuildCommand(this TCCommand command, params string[] parameters)
        {

            var attrib = command.GetAttribute<TCCommandAttribute>();

            if (attrib.ParamsNum > parameters.Length)
                throw new ArgumentOutOfRangeException("parameters");

            var sb = new StringBuilder(attrib.CommandName);

            if (parameters.Length > 0)
            {

                if (!attrib.CommandName.EndsWith(" "))
                    sb.Append(' ');

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i == parameters.Length - 1)
                        sb.Append(parameters[i]);
                    else
                        sb.Append(parameters[i] + " ");
                }

            }

            return sb.ToString();

        }

    }
}
