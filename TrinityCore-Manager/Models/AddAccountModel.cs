using Catel.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinityCore_Manager.Models
{
    public class AddAccountModel : ModelBase
    {

        public string AccountName
        {
            get
            {
                return GetValue<string>(AccountNameProperty);
            }
            set
            {
                SetValue(AccountNameProperty, value);
            }
        }

        public static readonly PropertyData AccountNameProperty = RegisterProperty("AccountName", typeof(string));

        public string AccountPassword
        {
            get
            {
                return GetValue<string>(AccountPasswordProperty);
            }
            set
            {
                SetValue(AccountPasswordProperty, value);
            }
        }

        public static readonly PropertyData AccountPasswordProperty = RegisterProperty("AccountPassword", typeof(string));

        public string AccountEmail

        {
            get
            {
                return GetValue<string>(AccountEmailProperty);
            }
            set
            {
                SetValue(AccountEmailProperty, value);
            }
        }

        public static readonly PropertyData AccountEmailProperty = RegisterProperty("AccountEmail", typeof(string));

        public string Rank
        {
            get
            {
                return GetValue<string>(RankProperty);
            }
            set
            {
                SetValue(RankProperty, value);
            }
        }

        public static readonly PropertyData RankProperty = RegisterProperty("Rank", typeof(string));

        public ObservableCollection<string> Ranks
        {
            get
            {
                return GetValue<ObservableCollection<string>>(RanksProperty);
            }
            set
            {
                SetValue(RanksProperty, value);
            }
        }

        public static readonly PropertyData RanksProperty = RegisterProperty("Ranks", typeof(ObservableCollection<string>), new ObservableCollection<string>
        {
            "Player",
            "GM"
        });

    }
}
