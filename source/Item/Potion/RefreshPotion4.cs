using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class RefreshPotion4 : BasicPotion {
		public RefreshPotion4() {
			this.itemImgPath += "Refresh4";
			this.itemName = "Refresh potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.hitPoints.Current += (int)Math.Round(player.hitPoints.Max * 1.0);
			player.manaPoints.Current += (int)Math.Round(player.manaPoints.Max * 1.0);
			return true;
		}
	}
}
