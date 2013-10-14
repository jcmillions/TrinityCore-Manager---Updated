using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinityCore_Manager.Models.Item
{
    [Serializable]
    public class ItemType
    {

        public string ItemName { get; set; }

        public int ItemId { get; set; }

        public ItemType()
        {
        }

        public ItemType(string name, int id)
        {
            ItemName = name;
            ItemId = id;
        }

    }
}
