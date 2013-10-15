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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;

namespace TrinityCore_Manager.Models
{
    [Serializable]
    public class WizardModel : ModelBase
    {

        public bool ConnectRemotely
        {
            get
            {
                return GetValue<bool>(ConnectRemotelyProperty);
            }
            set
            {
                SetValue(ConnectRemotelyProperty, value);
            }
        }

        public static readonly PropertyData ConnectRemotelyProperty = RegisterProperty("ConnectRemotely", typeof(bool));

        public bool ConnectLocally
        {
            get
            {
                return GetValue<bool>(ConnectLocallyProperty);
            }
            set
            {
                SetValue(ConnectLocallyProperty, value);
            }
        }

        public static readonly PropertyData ConnectLocallyProperty = RegisterProperty("ConnectLocally", typeof(bool), true);

        public string ServerFolderLocation
        {
            get
            {
                return GetValue<string>(ServerFolderLocationProperty);
            }
            set
            {
                SetValue(ServerFolderLocationProperty, value);
            }
        }

        public static readonly PropertyData ServerFolderLocationProperty = RegisterProperty("ServerFolderLocation", typeof(string));

        public string Host
        {
            get
            {
                return GetValue<string>(HostProperty);
            }
            set
            {
                SetValue(HostProperty, value);
            }
        }

        public static readonly PropertyData HostProperty = RegisterProperty("Host", typeof(string));

        public int Port
        {
            get
            {
                return GetValue<int>(PortProperty);
            }
            set
            {
                SetValue(PortProperty, value);
            }
        }

        public static readonly PropertyData PortProperty = RegisterProperty("Port", typeof(int), 3443);

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

        public static readonly PropertyData UsernameProperty = RegisterProperty("Username", typeof(string));

        public string Password
        {
            get
            {
                return GetValue<string>(PasswordProperty);
            }
            set
            {
                SetValue(PasswordProperty, value);
            }
        }

        public static readonly PropertyData PasswordProperty = RegisterProperty("Password", typeof(string));

        public string MySQLHost
        {
            get
            {
                return GetValue<string>(MySQLHostProperty);
            }
            set
            {
                SetValue(MySQLHostProperty, value);
            }
        }

        public static readonly PropertyData MySQLHostProperty = RegisterProperty("MySQLHost", typeof(string));

        public int MySQLPort
        {
            get
            {
                return GetValue<int>(MySQLPortProperty);
            }
            set
            {
                SetValue(MySQLPortProperty, value);
            }
        }

        public static readonly PropertyData MySQLPortProperty = RegisterProperty("MySQLPort", typeof(int), 3306);

        public string MySQLUsername
        {
            get
            {
                return GetValue<string>(MySQLUsernameProperty);
            }
            set
            {
                SetValue(MySQLUsernameProperty, value);
            }
        }

        public static readonly PropertyData MySQLUsernameProperty = RegisterProperty("MySQLUsername", typeof(string));

        public string MySQLPassword
        {
            get
            {
                return GetValue<string>(MySQLPasswordProperty);
            }
            set
            {
                SetValue(MySQLPasswordProperty, value);
            }
        }

        public static readonly PropertyData MySQLPasswordProperty = RegisterProperty("MySQLPassword", typeof(string));

        public string SelectedAuthDB
        {
            get
            {
                return GetValue<string>(SelectedAuthDBProperty);
            }
            set
            {
                SetValue(SelectedAuthDBProperty, value);
            }
        }

        public static readonly PropertyData SelectedAuthDBProperty = RegisterProperty("SelectedAuthDB", typeof(string));

        public string SelectedCharDB
        {
            get
            {
                return GetValue<string>(SelectedCharDBProperty);
            }
            set
            {
                SetValue(SelectedCharDBProperty, value);
            }
        }

        public static readonly PropertyData SelectedCharDBProperty = RegisterProperty("SelectedCharDB", typeof(string));

        public string SelectedWorldDB
        {
            get
            {
                return GetValue<string>(SelectedWorldDBProperty);
            }
            set
            {
                SetValue(SelectedWorldDBProperty, value);
            }
        }

        public static readonly PropertyData SelectedWorldDBProperty = RegisterProperty("SelectedWorldDB", typeof(string));

        public string TCMVersion
        {
            get
            {
                return GetValue<string>(TCMVersionProperty);
            }
            set
            {
                SetValue(TCMVersionProperty, value);
            }
        }

        public static readonly PropertyData TCMVersionProperty = RegisterProperty("TCMVersion", typeof(string));

    }
}
