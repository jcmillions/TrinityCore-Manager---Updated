using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinityCore_Manager.Models;
using TrinityCore_Manager.TCM;

namespace TrinityCore_Manager.ViewModels
{
    public class DatabaseAccountCleanupViewModel : ViewModelBase
    {

        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IMessageService _messageService;

        public Command DeleteCommand { get; private set; }

        public DatabaseAccountCleanupViewModel(DatabaseAccountCleanupModel model, IPleaseWaitService pleaseWaitService, IMessageService messageService)
        {

             _pleaseWaitService = pleaseWaitService;
            _messageService = messageService;

            DatabaseAccountCleanup = model;

            DeleteCommand = new Command(Delete);

        }

        private async void Delete()
        {

            _pleaseWaitService.Show("Deleting...");

            try
            {

                int acctsCleanedUp = await TCManager.Instance.AuthDatabase.CleanupAccounts(LastLoginCleanupDate);

                _messageService.ShowInformation(String.Format("{0} account(s) deleted", acctsCleanedUp));

            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }

            _pleaseWaitService.Hide();

        }

        [Model]
        public DatabaseAccountCleanupModel DatabaseAccountCleanup
        {
            get
            {
                return GetValue<DatabaseAccountCleanupModel>(DatabaseAccountCleanupProperty);
            }
            set
            {
                SetValue(DatabaseAccountCleanupProperty, value);
            }
        }

        public static readonly PropertyData DatabaseAccountCleanupProperty = RegisterProperty("DatabaseAccountCleanup", typeof(DatabaseAccountCleanupModel));

        [ViewModelToModel("DatabaseAccountCleanup")]
        public DateTime LastLoginCleanupDate
        {
            get
            {
                return GetValue<DateTime>(LastLoginCleanupDateProperty);
            }
            set
            {
                SetValue(LastLoginCleanupDateProperty, value);
            }
        }

        public static readonly PropertyData LastLoginCleanupDateProperty = RegisterProperty("LastLoginCleanupDate", typeof(DateTime), DateTime.Now);

    }
}
