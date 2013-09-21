using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;

namespace TrinityCore_Manager.Models
{
    public class FindItemModel : ModelBase
    {

        public FindItemModel()
        {
            Items = new ObservableCollection<ItemModel>();
        }

        public ObservableCollection<ItemModel> Items
        {
            get
            {
                return GetValue<ObservableCollection<ItemModel>>(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        public static readonly PropertyData ItemsProperty = RegisterProperty("Items", typeof(ObservableCollection<ItemModel>));

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

    }
}
