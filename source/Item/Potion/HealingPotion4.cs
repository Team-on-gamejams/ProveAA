using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Potion {
	class HealingPotion4 : BasicPotion {
		public HealingPotion4() {
			this.itemImgPath += "Healing4";
			this.itemName = "Healing potion";
		}

		public override bool CardUsed(Player player) {
			player.hitPoints.Current += (int)Math.Round(player.hitPoints.Max * 0.80);
			return true;
		}
	}
}
