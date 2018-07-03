using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Weapon {
	class Spear : BasicWeapon {
		public Spear() {
			itemImgPath += @"Spear";
			itemName = "Spear";
			dmgMod = 2;
		}
	}
}
