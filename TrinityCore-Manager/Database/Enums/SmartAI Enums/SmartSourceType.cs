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

namespace TrinityCore_Manager.Database.Enums.SmartAI_Enums
{
    public enum SmartSourceType
    {
        SMART_SCRIPT_TYPE_CREATURE = 0,
        SMART_SCRIPT_TYPE_GAMEOBJECT = 1,
        SMART_SCRIPT_TYPE_AREATRIGGER = 2,
        SMART_SCRIPT_TYPE_EVENT_NYI = 3,
        SMART_SCRIPT_TYPE_GOSSIP_NYI = 4,
        SMART_SCRIPT_TYPE_QUEST_NYI = 5,
        SMART_SCRIPT_TYPE_SPELL_NYI = 6,
        SMART_SCRIPT_TYPE_TRANSPORT_NYI = 7,
        SMART_SCRIPT_TYPE_INSTANCE_NYI = 8,
        SMART_SCRIPT_TYPE_TIMED_ACTIONLIST = 9
    }
}
