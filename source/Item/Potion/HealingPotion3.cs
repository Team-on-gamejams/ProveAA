using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Potion {
	class HealingPotion3 : BasicPotion {
		public HealingPotion3() {
			this.itemImgPath += "Healing3";
			this.itemName = "Healing potion";
		}

		public override bool CardUsed(Player player) {
			player.hitPoints.Current += (int)Math.Round(player.hitPoints.Max * 0.50);
			return true;
		}
	}
}
