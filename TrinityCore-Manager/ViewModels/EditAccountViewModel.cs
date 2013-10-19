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
using TrinityCore_Manager.Database.Enums;
using TrinityCore_Manager.Helpers;
using TrinityCore_Manager.Models;
using TrinityCore_Manager.TCM;
using TrinityCore_Manager.Views;

namespace TrinityCore_Manager.ViewModels
{
    public class EditAccountViewModel : ViewModelBase
    {

        private IMessageService _messageService;
        private IPleaseWaitService _pleaseWaitService;

        public Command SaveAccountCommand { get; private set; }

        public EditAccountViewModel(EditAccountModel model, IPleaseWaitService pleaseWaitService, IMessageService messageService)
        {

            EditAccount = model;

            _messageService = messageService;
            _pleaseWaitService = pleaseWaitService;

            SaveAccountCommand = new Command(SaveAccount);

        }

        public async void SaveAccount()
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

            _pleaseWaitService.Show("Modifying Account...");

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


            Account acct = await TCManager.Instance.AuthDatabase.GetAccount(AccountName);

            if (acct == null)
            {

                Console.WriteLine(AccountName);

                _pleaseWaitService.Hide();

                _messageService.ShowError("Account does not exist!");

                return;

            }

            await TCManager.Instance.AuthDatabase.EditAccount(acct.Id, AccountName, AccountPassword, AccountEmail, level, exp);

            _pleaseWaitService.Hide();

            SaveAndCloseViewModel();

        }

        [Model]
        public EditAccountModel EditAccount
        {
            get
            {
                return GetValue<EditAccountModel>(EditAccountProperty);
            }
            set
            {
                SetValue(EditAccountProperty, value);
            }
        }

        public static readonly PropertyData EditAccountProperty = RegisterProperty("EditAccount", typeof(EditAccountModel));

        [ViewModelToModel("EditAccount")]
        public string AccountName
        {
            get
            {
                return GetValue<string>(AccountNameProperty);
            }
            set
            {

                TCManager.Instance.AuthDatabase.SearchForAccount(value).ContinueWith(task =>
                {

                    var autoComplete = new ObservableCollection<AutoCompleteEntry>();

                    foreach (Account acct in task.Result)
                    {
                        autoComplete.Add(new AutoCompleteEntry(acct.Username, acct.Username));
                    }

                    AutoCompleteList = autoComplete;

                });

                SetValue(AccountNameProperty, value);
            
            }
        }

        public static readonly PropertyData AccountNameProperty = RegisterProperty("AccountName", typeof(string));

        [ViewModelToModel("EditAccount")]
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

        [ViewModelToModel("EditAccount")]
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

        [ViewModelToModel("EditAccount")]
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

        [ViewModelToModel("EditAccount")]
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

        [ViewModelToModel("EditAccount")]
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

        [ViewModelToModel("EditAccount")]
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

        public ObservableCollection<string> AccountsLike
        {
            get
            {
                return GetValue<ObservableCollection<string>>(AccountsLikeProperty);
            }
            set
            {
                SetValue(AccountsLikeProperty, value);
            }
        }

        public static readonly PropertyData AccountsLikeProperty = RegisterProperty("AccountsLike", typeof(ObservableCollection<string>));

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
