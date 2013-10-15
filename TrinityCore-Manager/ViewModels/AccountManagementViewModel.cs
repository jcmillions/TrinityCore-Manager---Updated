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
using Catel.MVVM;
using Catel.MVVM.Services;
using TrinityCore_Manager.Database.Classes;
using TrinityCore_Manager.Extensions;
using TrinityCore_Manager.Models;
using TrinityCore_Manager.TCM;

namespace TrinityCore_Manager.ViewModels
{
    public class AccountManagementViewModel : ViewModelBase
    {

        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IMessageService _messageService;


        public Command BanAccountCommand { get; private set; }

        public Command UnbanAccountCommand { get; private set; }

        public AccountManagementViewModel(AccountsModel model, IUIVisualizerService uiVisualizerService, IPleaseWaitService pleaseWaitService, IMessageService messageService)
        {

            Accounts = model;

            _uiVisualizerService = uiVisualizerService;
            _pleaseWaitService = pleaseWaitService;
            _messageService = messageService;

            BanAccountCommand = new Command(BanAccount);
            UnbanAccountCommand = new Command(UnbanAccount);

        }

        private async void Refresh()
        {

            _pleaseWaitService.Show("Refreshing...");

            List<AccountModel> accountList = new List<AccountModel>();

            List<BannedAccount> accts = await TCManager.Instance.AuthDatabase.GetBannedAccounts();

            foreach (var acct in accts)
            {
                accountList.Add(new AccountModel((await TCManager.Instance.AuthDatabase.GetAccount(acct.Id)).Username));
            }

            TheAccounts = new ObservableCollection<AccountModel>(accountList);

            _pleaseWaitService.Hide();

        }

        private async void BanAccount()
        {

            if (string.IsNullOrEmpty(UsernameText))
            {

                _messageService.ShowError("You must input a username to ban!");

                return;

            }

            _pleaseWaitService.Show("Banning Account...");

            Account acct = await TCManager.Instance.AuthDatabase.GetAccount(UsernameText);

            if (acct == null)
            {

                _pleaseWaitService.Hide();

                _messageService.ShowError("Could not find account!");

                return;

            }

            await TCManager.Instance.AuthDatabase.BanAccount(acct.Id, (int)DateTime.Now.ToUnixTimestamp(), (int)DateTime.Now.AddYears(1).ToUnixTimestamp(), "Admin", "");

            Refresh();

        }

        private async void UnbanAccount()
        {

            if (SelectedAccount == null)
            {

                _messageService.ShowError("No account selected!");

                return;

            }

            _pleaseWaitService.Show("Removing account ban...");

            Account acct = await TCManager.Instance.AuthDatabase.GetAccount(SelectedAccount.Username);

            if (acct == null)
            {

                _pleaseWaitService.Hide();

                _messageService.ShowError("Could not find account!");

                return;

            }

            await TCManager.Instance.AuthDatabase.RemoveAccountBan(acct.Id);

            Refresh();

        }

        [Model]
        public AccountsModel Accounts
        {
            get
            {
                return GetValue<AccountsModel>(AccountsProperty);
            }
            set
            {
                SetValue(AccountsProperty, value);
            }
        }

        public static readonly PropertyData AccountsProperty = RegisterProperty("Accounts", typeof(AccountsModel));


        [ViewModelToModel("Accounts")]
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


        public AccountModel SelectedAccount
        {
            get
            {
                return GetValue<AccountModel>(SelectedAccountProperty);
            }
            set
            {
                SetValue(SelectedAccountProperty, value);
            }
        }

        public static readonly PropertyData SelectedAccountProperty = RegisterProperty("SelectedAccount", typeof(AccountModel));

        public string UsernameText
        {
            get
            {
                return GetValue<string>(UsernameTextProperty);
            }
            set
            {
                SetValue(UsernameTextProperty, value);
            }
        }

        public static readonly PropertyData UsernameTextProperty = RegisterProperty("UsernameText", typeof(string));

    }
}
