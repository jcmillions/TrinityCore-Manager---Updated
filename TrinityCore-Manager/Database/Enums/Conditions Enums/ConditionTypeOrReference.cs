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
    public enum ConditionTypeOrReference
    {
        CONDITION_NONE = 0,
        CONDITION_AURA = 1,
        CONDITION_ITEM = 2,
        CONDITION_ITEM_EQUIPPED = 3,
        CONDITION_ZONEID = 4,
        CONDITION_REPUTATION_RANK = 5,
        CONDITION_TEAM = 6,
        CONDITION_SKILL = 7,
        CONDITION_QUESTREWARDED = 8,
        CONDITION_QUESTTAKEN = 9,
        CONDITION_DRUNKENSTATE = 10,
        CONDITION_WORLD_STATE = 11,
        CONDITION_ACTIVE_EVENT = 12,
        CONDITION_INSTANCE_INFO = 13,
        CONDITION_QUEST_NONE = 14,
        CONDITION_CLASS = 15,
        CONDITION_RACE = 16,
        CONDITION_ACHIEVEMENT = 17,
        CONDITION_TITLE = 18,
        CONDITION_SPAWNMASK = 19,
        CONDITION_GENDER = 20,
        CONDITION_UNIT_STATE = 21,
        CONDITION_MAPID = 22,
        CONDITION_AREAID = 23,
        CONDITION_UNUSED_24 = 24,
        CONDITION_SPELL = 25,
        CONDITION_PHASEMASK = 26,
        CONDITION_LEVEL = 27,
        CONDITION_QUEST_COMPLETE = 28,
        CONDITION_NEAR_CREATURE = 29,
        CONDITION_NEAR_GAMEOBJECT = 30,
        CONDITION_OBJECT_ENTRY = 31,
        CONDITION_TYPE_MASK = 32,
        CONDITION_RELATION_TO = 33,
        CONDITION_REACTION_TO = 34,
        CONDITION_DISTANCE_TO = 35,
        CONDITION_ALIVE = 36,
        CONDITION_HP_VAL = 37,
        CONDITION_HP_PCT = 38,
        CONDITION_MAX = 39
    }
}
