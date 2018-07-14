using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Potion {
	class HealingPotion2 : BasicPotion {
		public HealingPotion2() {
			this.itemImgPath += "Healing2";
			this.itemName = "Healing potion";
		}

		public override bool CardUsed(Player player) {
			player.hitPoints.Current += (int)Math.Round(player.hitPoints.Max * 0.20);
			return true;
		}
	}
}
