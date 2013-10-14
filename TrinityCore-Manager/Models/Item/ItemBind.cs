using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinityCore_Manager.Models.Item
{
    [Serializable]
    public class ItemBind
    {

        public string BindType { get; set; }

        public int BindId { get; set; }

        public ItemBind()
        {
        }

        public ItemBind(string type, int id)
        {
            BindType = type;
            BindId = id;
        }

    }
}
