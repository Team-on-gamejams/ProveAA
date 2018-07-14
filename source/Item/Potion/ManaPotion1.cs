using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class ManaPotion1 : BasicPotion {
		public ManaPotion1() {
			this.itemImgPath += "Mana1";
			this.itemName = "Mana potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.manaPoints.Current += (int)Math.Round(player.manaPoints.Max * 0.10);
			return true;
		}
	}
}
