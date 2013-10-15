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
using TrinityCore_Manager.Database.Enums;

namespace TrinityCore_Manager.Database.Classes
{
    public class TCCharacter
    {

        public int Guid { get; set; }
        public int Account { get; set; }
        public string Name { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterClass Class { get;  set; }
        public int Level { get; set; }
        public int Money { get; set; }
        public bool Online { get; set; }
        public int TotalTime { get; set; }
        public int TotalKills { get; set; }
        public int ArenaPoints { get; set; }
        public int TotalHonorsPoints { get; set; }

    }
}
