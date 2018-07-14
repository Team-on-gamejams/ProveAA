using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class RefreshPotion3 : BasicPotion {
		public RefreshPotion3() {
			this.itemImgPath += "Refresh3";
			this.itemName = "Refresh potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.hitPoints.Current += (int)Math.Round(player.hitPoints.Max * 0.60);
			player.manaPoints.Current += (int)Math.Round(player.manaPoints.Max * 0.60);
			return true;
		}
	}
}
