using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class ManaPotion3 : BasicPotion {
		public ManaPotion3() {
			this.itemImgPath += "Mana3";
			this.itemName = "Mana potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.manaPoints.Current += (int)Math.Round(player.manaPoints.Max * 0.50);
			return true;
		}
	}
}
