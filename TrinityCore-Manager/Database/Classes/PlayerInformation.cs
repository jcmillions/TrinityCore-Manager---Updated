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

namespace TrinityCore_Manager.Database.Classes
{
    class PlayerInformation
    {
        public string CharacterName { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastIp { get; set; }
        public string Email { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public string Map { get; set; }
        public string Money { get; set; }
        public string Account { get; set; }
        public string AccountId { get; set; }
        public string GmLevel { get; set; }
        public string PlayedTime { get; set; }
        public int Level { get; set; }
        public string Area { get; set; }
        public int Phase { get; set; }
    }
}
