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
    public class CharacterSelectingModel : ModelBase
    {
        public CharacterSelectingModel()
        {
            Characters = new ObservableCollection<string>();
        }

        public ObservableCollection<string> Characters
        {
            get
            {
                return GetValue<ObservableCollection<string>>(CharactersProperty);
            }
            set
            {
                SetValue(CharactersProperty, value);
            }
        }

        public static readonly PropertyData CharactersProperty = RegisterProperty("Characters", typeof(ObservableCollection<string>));

        public string SearchText
        {
            get
            {
                return GetValue<string>(SearchTextProperty);
            }
            set
            {
                SetValue(SearchTextProperty, value);
            }
        }

        public static readonly PropertyData SearchTextProperty = RegisterProperty("SearchText", typeof(string));

        public string SelectedCharacter
        {
            get
            {
                return GetValue<string>(SelectedCharacterProperty);
            }
            set
            {
                SetValue(SelectedCharacterProperty, value);
            }
        }

        public static readonly PropertyData SelectedCharacterProperty = RegisterProperty("SelectedCharacter", typeof(string));

    }
}
