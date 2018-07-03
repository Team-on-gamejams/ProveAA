using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Armor {
	class MetallShield : BasicArmor {
		public MetallShield() {
			itemImgPath += @"MetallShield";
			itemName = "Metall shield";
			armorMod = 2;
		}
	}
}
