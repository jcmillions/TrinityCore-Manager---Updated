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
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinityCore_Manager.Database.Enums;
using TrinityCore_Manager.Models;
using TrinityCore_Manager.TCM;

namespace TrinityCore_Manager.ViewModels
{
    public class AddAccountViewModel : ViewModelBase
    {

        private IMessageService _messageService;
        private IPleaseWaitService _pleaseWaitService;

        public Command CreateAccountCommand { get; private set; }

        public AddAccountViewModel(AddAccountModel model, IPleaseWaitService pleaseWaitService, IMessageService messageService)
        {

            AddAccount = model;

            _messageService = messageService;
            _pleaseWaitService = pleaseWaitService;

            CreateAccountCommand = new Command(CreateAccount);

        }

        private async void CreateAccount()
        {

            if (string.IsNullOrEmpty(AccountName) || string.IsNullOrEmpty(AccountPassword) || string.IsNullOrEmpty(Rank) || string.IsNullOrEmpty(Expansion))
            {

                _messageService.ShowError("All fields are required!");

                return;

            }


            if (AccountName.Length > 32)
            {

                _messageService.ShowError("Account name length exceeded!");

                return;

            }

            if (AccountEmail == null)
                AccountEmail = String.Empty;

            _pleaseWaitService.Show("Creating Account...");

            GMLevel level = GMLevel.Player;

            if (Rank == "GM")
                level = GMLevel.GM;
            else if (Rank == "Mod")
                level = GMLevel.Moderator;
            else if (Rank == "Head GM")
                level = GMLevel.HeadGM;
            else if (Rank == "Admin")
                level = GMLevel.Admin;

            Expansion exp = Database.Enums.Expansion.Vanilla;

            if (Expansion == "TBC")
                exp = Database.Enums.Expansion.TBC;
            else if (Expansion == "WOTLK")
                exp = Database.Enums.Expansion.WOTLK;

            await TCManager.Instance.AuthDatabase.CreateAccount(AccountName, AccountPassword, (int)level, (int)exp, AccountEmail);

            _pleaseWaitService.Hide();

            SaveAndCloseViewModel();

        }

        [Model]
        public AddAccountModel AddAccount
        {
            get
            {
                return GetValue<AddAccountModel>(AddAccountProperty);
            }
            set
            {
                SetValue(AddAccountProperty, value);
            }
        }

        public static readonly PropertyData AddAccountProperty = RegisterProperty("AddAccount", typeof(AddAccountModel));

        [ViewModelToModel("AddAccount")]
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

        [ViewModelToModel("AddAccount")]
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

        [ViewModelToModel("AddAccount")]
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

        [ViewModelToModel("AddAccount")]
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

        [ViewModelToModel("AddAccount")]
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
            "Mod",
            "GM",
            "Head GM",
            "Admin"
        });

        [ViewModelToModel("AddAccount")]
        public string Expansion
        {
            get
            {
                return GetValue<string>(ExpansionProperty);
            }
            set
            {
                SetValue(ExpansionProperty, value);
            }
        }

        public static readonly PropertyData ExpansionProperty = RegisterProperty("Expansion", typeof(string));

        [ViewModelToModel("AddAccount")]
        public ObservableCollection<string> Expansions
        {
            get
            {
                return GetValue<ObservableCollection<string>>(ExpansionsProperty);
            }
            set
            {
                SetValue(ExpansionsProperty, value);
            }
        }

        public static readonly PropertyData ExpansionsProperty = RegisterProperty("Expansions", typeof(ObservableCollection<string>), new ObservableCollection<string>
        {
            "Vanilla",
            "TBC",
            "WOTLK",
        });

    }
}
