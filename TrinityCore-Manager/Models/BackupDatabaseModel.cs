﻿// TrinityCore-Manager
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
using Catel.Data;

namespace TrinityCore_Manager.Models
{
    public class BackupDatabaseModel : ModelBase
    {
        public bool AuthSelected
        {
            get
            {
                return GetValue<bool>(AuthSelectedProperty);
            }
            set
            {
                SetValue(AuthSelectedProperty, value);
            }
        }

        public static readonly PropertyData AuthSelectedProperty = RegisterProperty("AuthSelected", typeof(bool));

        public bool CharSelected
        {
            get
            {
                return GetValue<bool>(CharSelectedProperty);
            }
            set
            {
                SetValue(CharSelectedProperty, value);
            }
        }

        public static readonly PropertyData CharSelectedProperty = RegisterProperty("CharSelected", typeof(bool));

        public bool WorldSelected
        {
            get
            {
                return GetValue<bool>(WorldSelectedProperty);
            }
            set
            {
                SetValue(WorldSelectedProperty, value);
            }
        }

        public static readonly PropertyData WorldSelectedProperty = RegisterProperty("WorldSelected", typeof(bool));

        public bool BackupsScheduled
        {
            get
            {
                return GetValue<bool>(BackupScheduledProperty);
            }
            set
            {
                SetValue(BackupScheduledProperty, value);
            }
        }

        public static PropertyData BackupScheduledProperty = RegisterProperty("BackupsScheduled", typeof(bool));

        public int BackupDays
        {
            get
            {
                return GetValue<int>(BackupDaysProperty);
            }
            set
            {
                SetValue(BackupDaysProperty, value);
            }
        }

        public static PropertyData BackupDaysProperty = RegisterProperty("BackupDays", typeof(int));


        public int BackupHours
        {
            get
            {
                return GetValue<int>(BackupHoursProperty);
            }
            set
            {
                SetValue(BackupHoursProperty, value);
            }
        }

        public static PropertyData BackupHoursProperty = RegisterProperty("BackupHours", typeof(int));


        public int BackupMinutes
        {
            get
            {
                return GetValue<int>(BackupMinutesProperty);
            }
            set
            {
                SetValue(BackupMinutesProperty, value);
            }
        }

        public static PropertyData BackupMinutesProperty = RegisterProperty("BackupMinutes", typeof(int));

    }
}
