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
using Catel.Data;
using TrinityCore_Manager.Models.Item;

namespace TrinityCore_Manager.Models
{
    public class AddEditItemModel : ModelBase
    {

        public AddEditItemModel()
        {
        }

        public int ItemId
        {
            get
            {
                return GetValue<int>(ItemIdProperty);
            }
            set
            {
                SetValue(ItemIdProperty, value);
            }
        }

        public static readonly PropertyData ItemIdProperty = RegisterProperty("ItemId", typeof(int));

        public ItemType IType
        {
            get
            {
                return GetValue<ItemType>(ITypeProperty);
            }
            set
            {
                SetValue(ITypeProperty, value);
            }
        }

        public static readonly PropertyData ITypeProperty = RegisterProperty("ItemType", typeof(ItemType));

        public ItemBind Bind
        {
            get
            {
                return GetValue<ItemBind>(BindProperty);
            }
            set
            {
                SetValue(BindProperty, value);
            }
        }

        public static readonly PropertyData BindProperty = RegisterProperty("Bind", typeof(ItemBind));

        public ObservableCollection<ItemType> ITypes
        {
            get
            {
                return GetValue<ObservableCollection<ItemType>>(ITypesProperty);
            }
            set
            {
                SetValue(ITypesProperty, value);
            }
        }

        public static readonly PropertyData ITypesProperty = RegisterProperty("ITypes", typeof(ObservableCollection<ItemType>), new ObservableCollection<ItemType>
        {
            new ItemType("Consumable", 0),
            new ItemType("Potion", 1),
            new ItemType("Elixir", 2),
            new ItemType("Flask", 3),
            new ItemType("Scroll", 4),
            new ItemType("Food/Drink", 5),
            new ItemType("Enchantment", 6),
            new ItemType("Bandage", 7),
            new ItemType("Other", 8)
        });

        public ObservableCollection<ItemBind> Binds
        {
            get
            {
                return GetValue<ObservableCollection<ItemBind>>(BindsProperty);
            }
            set
            {
                SetValue(BindsProperty, value);
            }
        }

        public static readonly PropertyData BindsProperty = RegisterProperty("Binds", typeof(ObservableCollection<ItemBind>), new ObservableCollection<ItemBind>
        {
            new ItemBind("None", 0),
            new ItemBind("On Pickup", 1),
            new ItemBind("On Use", 3)
        });

        public int BuyCopper
        {
            get
            {
                return GetValue<int>(BuyCopperProperty);
            }
            set
            {
                SetValue(BuyCopperProperty, value);
            }
        }

        public static readonly PropertyData BuyCopperProperty = RegisterProperty("BuyCopper", typeof(int));

        public int BuySilver
        {
            get
            {
                return GetValue<int>(BuySilverProperty);
            }
            set
            {
                SetValue(BuySilverProperty, value);
            }
        }

        public static readonly PropertyData BuySilverProperty = RegisterProperty("BuySilver", typeof(int));

        public int BuyGold
        {
            get
            {
                return GetValue<int>(BuyGoldProperty);
            }
            set
            {
                SetValue(BuyGoldProperty, value);
            }
        }

        public static readonly PropertyData BuyGoldProperty = RegisterProperty("BuyGold", typeof(int));

        public int SellCopper
        {
            get
            {
                return GetValue<int>(SellCopperProperty);
            }
            set
            {
                SetValue(SellCopperProperty, value);
            }
        }

        public static readonly PropertyData SellCopperProperty = RegisterProperty("SellCopper", typeof(int));

        public int SellSilver
        {
            get
            {
                return GetValue<int>(SellSilverProperty);
            }
            set
            {
                SetValue(SellSilverProperty, value);
            }
        }

        public static readonly PropertyData SellSilverProperty = RegisterProperty("SellSilver", typeof(int));

        public int SellGold
        {
            get
            {
                return GetValue<int>(SellGoldProperty);
            }
            set
            {
                SetValue(SellGoldProperty, value);
            }
        }

        public static readonly PropertyData SellGoldProperty = RegisterProperty("SellGold", typeof(int));

        public bool WarriorAllowed
        {
            get
            {
                return GetValue<bool>(WarriorAllowedProperty);
            }
            set
            {
                SetValue(WarriorAllowedProperty, value);
            }
        }

        public static readonly PropertyData WarriorAllowedProperty = RegisterProperty("WarriorAllowed", typeof(bool));

        public bool PaladinAllowed
        {
            get
            {
                return GetValue<bool>(PaladinAllowedProperty);
            }
            set
            {
                SetValue(PaladinAllowedProperty, value);
            }
        }

        public static readonly PropertyData PaladinAllowedProperty = RegisterProperty("PaladinAllowed", typeof(bool));

        public bool HunterAllowed
        {
            get
            {
                return GetValue<bool>(HunterAllowedProperty);
            }
            set
            {
                SetValue(HunterAllowedProperty, value);
            }
        }

        public static readonly PropertyData HunterAllowedProperty = RegisterProperty("HunterAllowed", typeof(bool));


        public bool RogueAllowed
        {
            get
            {
                return GetValue<bool>(RogueAllowedProperty);
            }
            set
            {
                SetValue(RogueAllowedProperty, value);
            }
        }

        public static readonly PropertyData RogueAllowedProperty = RegisterProperty("RogueAllowed", typeof(bool));

        public bool PriestAllowed
        {
            get
            {
                return GetValue<bool>(PriestAllowedProperty);
            }
            set
            {
                SetValue(PriestAllowedProperty, value);
            }
        }

        public static readonly PropertyData PriestAllowedProperty = RegisterProperty("PriestAllowed", typeof(bool));

        public bool DruidAllowed
        {
            get
            {
                return GetValue<bool>(DruidAllowedProperty);
            }
            set
            {
                SetValue(DruidAllowedProperty, value);
            }
        }

        public static readonly PropertyData DruidAllowedProperty = RegisterProperty("DruidAllowed", typeof(bool));

        public bool ShamanAllowed
        {
            get
            {
                return GetValue<bool>(ShamanAllowedProperty);
            }
            set
            {
                SetValue(ShamanAllowedProperty, value);
            }
        }

        public static readonly PropertyData ShamanAllowedProperty = RegisterProperty("ShamanAllowed", typeof(bool));

        public bool MageAllowed
        {
            get
            {
                return GetValue<bool>(MageAllowedProperty);
            }
            set
            {
                SetValue(MageAllowedProperty, value);
            }
        }

        public static readonly PropertyData MageAllowedProperty = RegisterProperty("MageAllowed", typeof(bool));

        public bool WarlockAllowed
        {
            get
            {
                return GetValue<bool>(WarlockAllowedProperty);
            }
            set
            {
                SetValue(WarlockAllowedProperty, value);
            }
        }

        public static readonly PropertyData WarlockAllowedProperty = RegisterProperty("WarlockAllowed", typeof(bool));

        public bool DeathKnightAllowed
        {
            get
            {
                return GetValue<bool>(DeathKnightProperty);
            }
            set
            {
                SetValue(DeathKnightProperty, value);
            }
        }

        public static readonly PropertyData DeathKnightProperty = RegisterProperty("DeathKnightAllowed", typeof(bool));

        public bool HumanAllowed
        {
            get
            {
                return GetValue<bool>(HumanAllowedProperty);
            }
            set
            {
                SetValue(HumanAllowedProperty, value);
            }
        }

        public static readonly PropertyData HumanAllowedProperty = RegisterProperty("HumanAllowed", typeof(bool));

        public bool OrcAllowed
        {
            get
            {
                return GetValue<bool>(OrcAllowedProperty);
            }
            set
            {
                SetValue(OrcAllowedProperty, value);
            }
        }

        public static readonly PropertyData OrcAllowedProperty = RegisterProperty("OrcAllowed", typeof(bool));

        public bool DwarfAllowed
        {
            get
            {
                return GetValue<bool>(DwarfAllowedProperty);
            }
            set
            {
                SetValue(DwarfAllowedProperty, value);
            }
        }

        public static readonly PropertyData DwarfAllowedProperty = RegisterProperty("DwarfAllowed", typeof(bool));

        public bool NightElfAllowed
        {
            get
            {
                return GetValue<bool>(NightElfAllowedProperty);
            }
            set
            {
                SetValue(NightElfAllowedProperty, value);
            }
        }

        public static readonly PropertyData NightElfAllowedProperty = RegisterProperty("NightElfAllowed", typeof(bool));

        public bool UndeadAllowed
        {
            get
            {
                return GetValue<bool>(UndeadAllowedProperty);
            }
            set
            {
                SetValue(UndeadAllowedProperty, value);
            }
        }

        public static readonly PropertyData UndeadAllowedProperty = RegisterProperty("UndeadAllowed", typeof(bool));

        public bool TaurenAllowed
        {
            get
            {
                return GetValue<bool>(TaurenAllowedProperty);
            }
            set
            {
                SetValue(TaurenAllowedProperty, value);
            }
        }

        public static readonly PropertyData TaurenAllowedProperty = RegisterProperty("TaurenAllowed", typeof(bool));

        public bool GnomeAllowed
        {
            get
            {
                return GetValue<bool>(GnomeAllowedProperty);
            }
            set
            {
                SetValue(GnomeAllowedProperty, value);
            }
        }

        public static readonly PropertyData GnomeAllowedProperty = RegisterProperty("GnomeAllowed", typeof(bool));

        public bool TrollAllowed
        {
            get
            {
                return GetValue<bool>(TrollAllowedProperty);
            }
            set
            {
                SetValue(TrollAllowedProperty, value);
            }
        }

        public static readonly PropertyData TrollAllowedProperty = RegisterProperty("TrollAllowed", typeof(bool));

        public bool BloodElfAllowed
        {
            get
            {
                return GetValue<bool>(BloodElfAllowedProperty);
            }
            set
            {
                SetValue(BloodElfAllowedProperty, value);
            }
        }

        public static readonly PropertyData BloodElfAllowedProperty = RegisterProperty("BloodElfAllowed", typeof(bool));

        public bool DraeneiAllowed
        {
            get
            {
                return GetValue<bool>(DraeneiAllowedProperty);
            }
            set
            {
                SetValue(DraeneiAllowedProperty, value);
            }
        }

        public static readonly PropertyData DraeneiAllowedProperty = RegisterProperty("DraeneiAllowed", typeof(bool));

        public ObservableCollection<SkillProfessionModel> SkillProfessions
        {
            get
            {
                return GetValue<ObservableCollection<SkillProfessionModel>>(SkillProfessionsProperty);
            }
            set
            {
                SetValue(SkillProfessionsProperty, value);
            }
        }

        public static readonly PropertyData SkillProfessionsProperty = RegisterProperty("SkillProfressions", typeof(ObservableCollection<SkillProfessionModel>),
            new ObservableCollection<SkillProfessionModel>
            {
                new SkillProfessionModel("None", -1),
                new SkillProfessionModel("First Aid", 129),
                new SkillProfessionModel("Blacksmithing", 164),
                new SkillProfessionModel("Leather Working", 165),
                new SkillProfessionModel("Alchemy", 171),
                new SkillProfessionModel("Herbalism", 182),
                new SkillProfessionModel("Cooking", 185),
                new SkillProfessionModel("Mining", 186),
                new SkillProfessionModel("Tailoring", 197),
                new SkillProfessionModel("Engineering", 202),
                new SkillProfessionModel("Enchanting", 333),
                new SkillProfessionModel("Fishing", 356),
                new SkillProfessionModel("Skinning", 755),
                new SkillProfessionModel("Jewel Crafting", 0),
                new SkillProfessionModel("Inscription", 43),
                new SkillProfessionModel("Swords", 44),
                new SkillProfessionModel("Axes", 46),
                new SkillProfessionModel("Guns", 54),
                new SkillProfessionModel("Maces", 95),
                new SkillProfessionModel("Defense", 136),
                new SkillProfessionModel("Staves", 160),
                new SkillProfessionModel("Two-Handed Maces", 162),
                new SkillProfessionModel("Unarmed", 172),
                new SkillProfessionModel("Daggers", 173),
                new SkillProfessionModel("Thrown", 176),
                new SkillProfessionModel("Crossbows", 226),
                new SkillProfessionModel("Wands", 228),
                new SkillProfessionModel("Polearms", 229),
                new SkillProfessionModel("First", 473),
                new SkillProfessionModel("Riding", 762)
            });
    }
}
