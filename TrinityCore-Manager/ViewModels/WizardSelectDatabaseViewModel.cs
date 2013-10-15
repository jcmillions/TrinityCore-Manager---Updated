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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Services;
using MySql.Data.MySqlClient;
using TrinityCore_Manager.Models;

namespace TrinityCore_Manager.ViewModels
{
    public class WizardSelectDatabaseViewModel : ViewModelBase
    {

        public Command OkCommand { get; private set; }

        public Command CancelCommand { get; private set; }

        public WizardSelectDatabaseViewModel(WizardSelectDatabaseModel model, string host, int port, string username, string password)
        {

            WizardSelectDatabase = model;

            OkCommand = new Command(Ok);
            CancelCommand = new Command(CancelCmd);

            var connStr = new MySqlConnectionStringBuilder();
            connStr.Server = host;
            connStr.Port = (uint)port;
            connStr.UserID = username;
            connStr.Password = password;

            using (var connection = new MySqlConnection(connStr.ToString()))
            {
                connection.Open();

                using (var returnVal = new MySqlDataAdapter("SHOW DATABASES", connection))
                {

                    var dataTable = new DataTable();
                    returnVal.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                        for (int i = 0; i < row.ItemArray.Length; i++)
                            Databases.Add(row.ItemArray[i].ToString());

                }

            }

        }

        private void Ok()
        {
            this.SaveAndCloseViewModel();
        }

        private void CancelCmd()
        {
            this.CancelAndCloseViewModel();
        }

        [Model]
        public WizardSelectDatabaseModel WizardSelectDatabase
        {
            get
            {
                return GetValue<WizardSelectDatabaseModel>(WizardSelectDatabaseProperty);
            }
            set
            {
                SetValue(WizardSelectDatabaseProperty, value);
            }
        }

        public static readonly PropertyData WizardSelectDatabaseProperty = RegisterProperty("WizardSelectDatabase", typeof(WizardSelectDatabaseModel));

        [ViewModelToModel("WizardSelectDatabase")]
        public ObservableCollection<string> Databases
        {
            get
            {
                return GetValue<ObservableCollection<string>>(DatabasesProperty);
            }
            set
            {
                SetValue(DatabasesProperty, value);
            }
        }

        public static readonly PropertyData DatabasesProperty = RegisterProperty("Databases", typeof(ObservableCollection<string>));

        [ViewModelToModel("WizardSelectDatabase")]
        public string SelectedDatabaseName
        {
            get
            {
                return GetValue<string>(SelectedDatabaseNameProperty);
            }
            set
            {
                SetValue(SelectedDatabaseNameProperty, value);
            }
        }

        public static readonly PropertyData SelectedDatabaseNameProperty = RegisterProperty("SelectedDatabaseName", typeof(string));

    }
}
