using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Potion {
	class HealingPotion : BasicPotion {
		public HealingPotion() {
			this.itemImgPath += "Healing";
			this.itemName = "Healing potion";
		}

		public override bool CardUsed(Player player) {
			player.hitPoints.Current += (byte)(player.hitPoints.Max * Game.Settings.potion_Healing_HealPersent);
			return true;
		}
	}
}
