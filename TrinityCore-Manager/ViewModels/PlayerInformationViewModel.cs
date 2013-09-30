using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using TrinityCore_Manager.Models;

namespace TrinityCore_Manager.ViewModels
{
    public class PlayerInformationViewModel : ViewModelBase
    {

        public PlayerInformationViewModel(PlayerInformationModel model)
        {
            PlayerInformation = model;
        }

        [Model]
        public PlayerInformationModel PlayerInformation
        {
            get
            {
                return GetValue<PlayerInformationModel>(PlayerInformationProperty);
            }
            set
            {
                SetValue(PlayerInformationProperty, value);
            }
        }

        public static readonly PropertyData PlayerInformationProperty = RegisterProperty("PlayerInformation", typeof(PlayerInformationModel));

        [ViewModelToModel("PlayerInformation")]
        public string CharacterName
        {
            get
            {
                return GetValue<string>(CharacterNameProperty);
            }
            set
            {
                SetValue(CharacterNameProperty, value);
            }
        }

        public static readonly PropertyData CharacterNameProperty = RegisterProperty("CharacterName", typeof(string));


        [ViewModelToModel("PlayerInformation")]
        public string LastLogin
        {
            get
            {
                return GetValue<string>(LastLoginProperty);
            }
            set
            {
                SetValue(LastLoginProperty, value);
            }
        }

        public static readonly PropertyData LastLoginProperty = RegisterProperty("LastLogin", typeof(string));

        [ViewModelToModel("PlayerInformation")]
        public string LastIp
        {
            get
            {
                return GetValue<string>(LastIpProperty);
            }
            set
            {
                SetValue(LastIpProperty, value);
            }
        }

        public static readonly PropertyData LastIpProperty = RegisterProperty("LastIp", typeof(string));

        [ViewModelToModel("PlayerInformation")]
        public string Email
        {
            get
            {
                return GetValue<string>(EmailProperty);
            }
            set
            {
                SetValue(EmailProperty, value);
            }
        }

        public static readonly PropertyData EmailProperty = RegisterProperty("Email", typeof(string));

        [ViewModelToModel("PlayerInformation")]
        public string Race
        {
            get
            {
                return GetValue<string>(RaceProperty);
            }
            set
            {
                SetValue(RaceProperty, value);
            }
        }

        public static readonly PropertyData RaceProperty = RegisterProperty("Race", typeof(string));

        [ViewModelToModel("PlayerInformation")]
        public string Class
        {
            get
            {
                return GetValue<string>(ClassProperty);
            }
            set
            {
                SetValue(ClassProperty, value);
            }
        }

        public static readonly PropertyData ClassProperty = RegisterProperty("Class", typeof(string));

        [ViewModelToModel("PlayerInformation")]
        public string Money
        {
            get
            {
                return GetValue<string>(MoneyProperty);
            }
            set
            {
                SetValue(MoneyProperty, value);
            }
        }

        public static readonly PropertyData MoneyProperty = RegisterProperty("Money", typeof(string));

        [ViewModelToModel("PlayerInformation")]
        public string TotalKills
        {
            get
            {
                return GetValue<string>(TotalKillsProperty);
            }
            set
            {
                SetValue(TotalKillsProperty, value);
            }
        }

        public static readonly PropertyData TotalKillsProperty = RegisterProperty("TotalKills", typeof(string));

        [ViewModelToModel("PlayerInformation")]
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

        [ViewModelToModel("PlayerInformation")]
        public string AccountId
        {
            get
            {
                return GetValue<string>(AccountIdProperty);
            }
            set
            {
                SetValue(AccountIdProperty, value);
            }
        }

        public static readonly PropertyData AccountIdProperty = RegisterProperty("AccountId", typeof(string));

        [ViewModelToModel("PlayerInformation")]
        public string GMLevel
        {
            get
            {
                return GetValue<string>(GMLevelProperty);
            }
            set
            {
                SetValue(GMLevelProperty, value);
            }
        }

        public static readonly PropertyData GMLevelProperty = RegisterProperty("GMLevel", typeof(string));

        [ViewModelToModel("PlayerInformation")]
        public string PlayedTime
        {
            get
            {
                return GetValue<string>(PlayedTimeProperty);
            }
            set
            {
                SetValue(PlayedTimeProperty, value);
            }
        }

        public static readonly PropertyData PlayedTimeProperty = RegisterProperty("PlayedTime", typeof(string));

        [ViewModelToModel("PlayerInformation")]
        public string Level
        {
            get
            {
                return GetValue<string>(LevelProperty);
            }
            set
            {
                SetValue(LevelProperty, value);
            }
        }

        public static readonly PropertyData LevelProperty = RegisterProperty("Level", typeof(string));

    }
}
