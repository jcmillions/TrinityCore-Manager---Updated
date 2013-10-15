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
    class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public string LastIp { get; set; }
        public int FailedLogins { get; set; }
        public int Locked { get; set; }
        public string LockCountry { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Online { get; set; }
        public Expansion Exp { get; set; }
    }
}
