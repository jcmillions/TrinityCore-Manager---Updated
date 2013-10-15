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
using Catel.Collections;
using Catel.Data;
using Catel.MVVM;
using Catel.MVVM.Services;
using TrinityCore_Manager.Database.Classes;
using TrinityCore_Manager.Models;
using TrinityCore_Manager.TCM;

namespace TrinityCore_Manager.ViewModels
{
    public class IPManagementViewModel : ViewModelBase
    {

        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IMessageService _messageService;

        public Command BanIPCommand { get; private set; }

        public Command UnbanIPCommand { get; private set; }

        public IPManagementViewModel(IPManagementModel model, IUIVisualizerService uiVisualizerService, IPleaseWaitService pleaseWaitService, IMessageService messageService)
        {

            IPManagement = model;

            _uiVisualizerService = uiVisualizerService;
            _pleaseWaitService = pleaseWaitService;
            _messageService = messageService;

            BanIPCommand = new Command(BanIP);
            UnbanIPCommand = new Command(UnbanIP);

        }

        private async void BanIP()
        {

            if (string.IsNullOrEmpty(IP))
            {

                _messageService.ShowError("You must input a IP address to ban!");

                return;

            }

            _pleaseWaitService.Show();

            await TCManager.Instance.AuthDatabase.AddIpBan(IP, DateTime.Now, DateTime.Now.AddYears(1), "Admin", "");

            List<IPBan> bannedIPs = await TCManager.Instance.AuthDatabase.GetIPBans();

            IPs.Clear();
            IPs.AddRange(bannedIPs.Select(p => new IPModel { IPAddress = p.IP }));

            _pleaseWaitService.Hide();

        }

        private async void UnbanIP()
        {

            if (SelectedIP == null)
            {

                _messageService.ShowError("No IP Address selected!");

                return;

            }

            await TCManager.Instance.AuthDatabase.RemoveIpBan(SelectedIP.IPAddress);

            _pleaseWaitService.Show();

            List<IPBan> bannedIPs = await TCManager.Instance.AuthDatabase.GetIPBans();

            IPs.Clear();
            IPs.AddRange(bannedIPs.Select(p => new IPModel { IPAddress = p.IP }));

            _pleaseWaitService.Hide();

        }


        [Model]
        public IPManagementModel IPManagement
        {
            get
            {
                return GetValue<IPManagementModel>(IPManagementProperty);
            }
            set
            {
                SetValue(IPManagementProperty, value);
            }
        }

        public static readonly PropertyData IPManagementProperty = RegisterProperty("IPManagement", typeof(IPManagementModel));

        [ViewModelToModel("IPManagement")]
        public ObservableCollection<IPModel> IPs
        {
            get
            {
                return GetValue<ObservableCollection<IPModel>>(IPsProperty);
            }
            set
            {
                SetValue(IPsProperty, value);
            }
        }

        public static readonly PropertyData IPsProperty = RegisterProperty("IPs", typeof(ObservableCollection<IPModel>));


        public string IP
        {
            get
            {
                return GetValue<string>(IPProperty);
            }
            set
            {
                SetValue(IPProperty, value);
            }
        }

        public static readonly PropertyData IPProperty = RegisterProperty("IP", typeof(string));

        public IPModel SelectedIP
        {
            get
            {
                return GetValue<IPModel>(SelectedIPProperty);
            }
            set
            {
                SetValue(SelectedIPProperty, value);
            }
        }

        public static readonly PropertyData SelectedIPProperty = RegisterProperty("SelectedIP", typeof(IPModel));

    }
}
