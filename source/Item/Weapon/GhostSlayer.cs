using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Weapon {
	class GhostSlayer : BasicWeapon, Attributes.GhostKiller {
		public GhostSlayer() {
			itemImgPath += @"GhostSlayer";
			itemName = "Ghost Slayer";
			dmgMod = 0;
		}
	}
}
