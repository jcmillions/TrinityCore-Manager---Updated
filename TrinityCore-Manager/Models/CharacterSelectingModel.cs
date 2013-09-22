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
