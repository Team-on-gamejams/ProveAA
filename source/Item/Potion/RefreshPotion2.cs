using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class RefreshPotion2 : BasicPotion {
		public RefreshPotion2() {
			this.itemImgPath += "Refresh2";
			this.itemName = "Refresh potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.hitPoints.Current += (int)Math.Round(player.hitPoints.Max * 0.25);
			player.manaPoints.Current += (int)Math.Round(player.manaPoints.Max * 0.25);
			return true;
		}
	}
}
