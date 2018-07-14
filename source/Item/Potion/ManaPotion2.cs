using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class ManaPotion2 : BasicPotion {
		public ManaPotion2() {
			this.itemImgPath += "Mana2";
			this.itemName = "Mana potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.manaPoints.Current += (int)Math.Round(player.manaPoints.Max * 0.20);
			return true;
		}
	}
}
