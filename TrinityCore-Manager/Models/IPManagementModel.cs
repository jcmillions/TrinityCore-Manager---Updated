using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;

namespace TrinityCore_Manager.Models
{
    public class IPManagementModel : ModelBase
    {

        public IPManagementModel()
        {
        }

        public IPManagementModel(List<IPModel> ips)
        {
            IPs = new ObservableCollection<IPModel>(ips);
        }


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

    }
}
