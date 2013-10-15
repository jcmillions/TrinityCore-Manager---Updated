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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinityCore_Manager.Database.Enums
{
    public enum GuildRanks
    {

        GR_RIGHT_EMPTY = 64,
        GR_RIGHT_GCHATLISTEN = 65,
        GR_RIGHT_GCHATSPEAK = 66,
        GR_RIGHT_OFFCHATLISTEN = 68,
        GR_RIGHT_OFFCHATSPEAK = 72,
        GR_RIGHT_INVITE = 80,
        GR_RIGHT_REMOVE = 96,
        GR_RIGHT_PROMOTE = 192,
        GR_RIGHT_DEMOTE = 320,
        GR_RIGHT_SETMOTD = 4160,
        GR_RIGHT_EPNOTE = 8256,
        GR_RIGHT_VIEWOFFNOTE = 16448,
        GR_RIGHT_EOFFNOTE = 32832,
        GR_RIGHT_MODIFY_GUILD_INFO = 65600,
        GR_RIGHT_WITHDRAW_GOLD_LOCK = 131072,
        GR_RIGHT_WITHDRAW_REPAIR = 262144,
        GR_RIGHT_WITHDRAW_GOLD = 524288,
        GR_RIGHT_CREATE_GUILD_EVENT = 1048576,
        GR_RIGHT_ALL = 1962495

    }
}
