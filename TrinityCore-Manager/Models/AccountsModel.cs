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
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;

namespace TrinityCore_Manager.Models
{
    [Serializable]
    public class AccountsModel : ModelBase
    {

        public AccountsModel()
        {
            TheAccounts = new ObservableCollection<AccountModel>();
        }

        public AccountsModel(IEnumerable<AccountModel> accounts)
        {
            TheAccounts = new ObservableCollection<AccountModel>(accounts);
        }

        protected AccountsModel(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public ObservableCollection<AccountModel> TheAccounts
        {
            get
            {
                return GetValue<ObservableCollection<AccountModel>>(TheAccountsProperty);
            }
            set
            {
                SetValue(TheAccountsProperty, value);
            }
        }

        public static readonly PropertyData TheAccountsProperty = RegisterProperty("TheAccounts", typeof(ObservableCollection<AccountModel>));

    }
}
