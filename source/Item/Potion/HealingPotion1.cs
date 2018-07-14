using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Potion {
	class HealingPotion1 : BasicPotion {
		public HealingPotion1() {
			this.itemImgPath += "Healing1";
			this.itemName = "Healing potion";
		}

		public override bool CardUsed(Player player) {
			player.hitPoints.Current += (int)Math.Round(player.hitPoints.Max * 0.10);
			return true;
		}
	}
}
