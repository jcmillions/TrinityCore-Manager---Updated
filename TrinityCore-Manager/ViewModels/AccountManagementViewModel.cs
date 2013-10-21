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
using TrinityCore_Manager.Helpers;
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

        public AccountManagementViewModel(AccountsManagementModel model, IUIVisualizerService uiVisualizerService, IPleaseWaitService pleaseWaitService, IMessageService messageService)
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

            List<AccountManagementModel> accountList = new List<AccountManagementModel>();

            List<BannedAccount> accts = await TCManager.Instance.AuthDatabase.GetBannedAccounts();

            foreach (var acct in accts)
            {

                var account = await TCManager.Instance.AuthDatabase.GetAccount(acct.Id);
                var ban = await TCManager.Instance.AuthDatabase.GetBannedAccount(acct.Id);

                accountList.Add(new AccountManagementModel(account.Username, ban.BanDate, ban.BanReason));

            }

            TheAccounts = new ObservableCollection<AccountManagementModel>(accountList);

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

            await TCManager.Instance.AuthDatabase.BanAccount(acct.Id, (int)DateTime.Now.ToUnixTimestamp(), (int)DateTime.Now.AddYears(1).ToUnixTimestamp(), "Admin", BanReasonText);

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
        public AccountsManagementModel Accounts
        {
            get
            {
                return GetValue<AccountsManagementModel>(AccountsProperty);
            }
            set
            {
                SetValue(AccountsProperty, value);
            }
        }

        public static readonly PropertyData AccountsProperty = RegisterProperty("Accounts", typeof(AccountsManagementModel));


        [ViewModelToModel("Accounts")]
        public ObservableCollection<AccountManagementModel> TheAccounts
        {
            get
            {
                return GetValue<ObservableCollection<AccountManagementModel>>(TheAccountsProperty);
            }
            set
            {
                SetValue(TheAccountsProperty, value);
            }
        }

        public static readonly PropertyData TheAccountsProperty = RegisterProperty("TheAccounts", typeof(ObservableCollection<AccountManagementModel>));


        public AccountManagementModel SelectedAccount
        {
            get
            {
                return GetValue<AccountManagementModel>(SelectedAccountProperty);
            }
            set
            {
                SetValue(SelectedAccountProperty, value);
            }
        }

        public static readonly PropertyData SelectedAccountProperty = RegisterProperty("SelectedAccount", typeof(AccountManagementModel));

        public string UsernameText
        {
            get
            {
                return GetValue<string>(UsernameTextProperty);
            }
            set
            {

                TCManager.Instance.AuthDatabase.SearchForAccount(value).ContinueWith(task =>
                {

                    var acl = new ObservableCollection<AutoCompleteEntry>();

                    List<Account> accts = task.Result;

                    foreach (var acct in accts)
                    {
                        acl.Add(new AutoCompleteEntry(acct.Username, acct.Username));
                    }

                    AutoCompleteList = acl;

                });

                SetValue(UsernameTextProperty, value);
            
            }
        }

        public static readonly PropertyData UsernameTextProperty = RegisterProperty("UsernameText", typeof(string));

        public string BanReasonText
        {
            get
            {
                return GetValue<string>(BanReasonTextProperty);
            }
            set
            {
                SetValue(BanReasonTextProperty, value);
            }
        }

        public static readonly PropertyData BanReasonTextProperty = RegisterProperty("BanReasonText", typeof(string));

        public ObservableCollection<AutoCompleteEntry> AutoCompleteList
        {
            get
            {
                return GetValue<ObservableCollection<AutoCompleteEntry>>(AutoCompleteListProperty);
            }
            set
            {
                SetValue(AutoCompleteListProperty, value);
            }
        }

        public static readonly PropertyData AutoCompleteListProperty = RegisterProperty("AutoCompleteList", typeof(ObservableCollection<AutoCompleteEntry>));

    }
}
