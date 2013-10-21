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
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;

namespace TrinityCore_Manager.Models
{
    [Serializable]
    public class AccountManagementModel : ModelBase
    {

        public AccountManagementModel(string username, DateTime banDate, string banReason = "")
        {
            Username = username;
            BanDate = banDate.ToString("MM-dd-yyyy");
            BanReason = banReason;
        }

        protected AccountManagementModel(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public string Username
        {
            get
            {
                return GetValue<string>(UsernameProperty);
            }
            set
            {
                SetValue(UsernameProperty, value);
            }
        }

        public static readonly PropertyData UsernameProperty = RegisterProperty("Username", typeof(string), string.Empty);

        public string BanReason
        {
            get
            {
                return GetValue<string>(BanReasonProperty);
            }
            set
            {
                SetValue(BanReasonProperty, value);
            }
        }

        public static readonly PropertyData BanReasonProperty = RegisterProperty("BanReason", typeof(string));

        public string BanDate
        {
            get
            {
                return GetValue<string>(BanDateProperty);
            }
            set
            {
                SetValue(BanDateProperty, value);
            }
        }

        public static readonly PropertyData BanDateProperty = RegisterProperty("BanDate", typeof(string));

    }
}
