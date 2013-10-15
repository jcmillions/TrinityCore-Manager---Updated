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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;

namespace TrinityCore_Manager.Models
{
    public class SettingsModel : ModelBase
    {
        public SettingsModel()
        {
            Themes = new ObservableCollection<string>();
        }

        public ObservableCollection<string> Themes
        {
            get
            {
                return GetValue<ObservableCollection<string>>(ThemesProperty);
            }
            set
            {
                SetValue(ThemesProperty, value);
            }
        }

        public static readonly PropertyData ThemesProperty = RegisterProperty("Themes", typeof(ObservableCollection<string>));

        public string SelectedTheme
        {
            get
            {
                return GetValue<string>(SelectedThemeProperty);
            }
            set
            {
                SetValue(SelectedThemeProperty, value);
            }
        }

        public static readonly PropertyData SelectedThemeProperty = RegisterProperty("SelectedTheme", typeof(string), "Silver");

    }
}
