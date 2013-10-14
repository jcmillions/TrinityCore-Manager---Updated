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

            _pleaseWaitService.Show("Creating Account...");

            GMLevel level = GMLevel.Player;

            if (Rank == "GM")
                level = GMLevel.GM;

            await TCManager.Instance.AuthDatabase.CreateAccount(AccountName, AccountPassword, (int)level, -1);

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
            "GM"
        });

    }
}
