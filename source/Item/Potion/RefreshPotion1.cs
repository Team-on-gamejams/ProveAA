using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class RefreshPotion1 : BasicPotion {
		public RefreshPotion1() {
			this.itemImgPath += "Refresh1";
			this.itemName = "Refresh potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.hitPoints.Current += (int)Math.Round(player.hitPoints.Max * 0.10);
			player.manaPoints.Current += (int)Math.Round(player.manaPoints.Max * 0.10);
			return true;
		}
	}
}
