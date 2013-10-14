using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinityCore_Manager.Models.Item
{
    [Serializable]
    public class SkillProfessionModel
    {

        public SkillProfessionModel()
        {
        }

        public SkillProfessionModel(string name, int skillId)
        {
            Name = name;
            RequiredSkillId = skillId;
        }
        
        public string Name { get; set; }

        public int RequiredSkillId { get; set; }

    }
}
