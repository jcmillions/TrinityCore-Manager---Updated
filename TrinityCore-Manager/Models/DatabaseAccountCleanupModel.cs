using Catel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinityCore_Manager.Models
{
    public class DatabaseAccountCleanupModel : ModelBase
    {

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
