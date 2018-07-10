using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class RefreshPotion : BasicPotion {
		public RefreshPotion() {
			this.itemImgPath += "Refresh";
			this.itemName = "Refresh potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.hitPoints.Current += (int)Math.Round(player.hitPoints.Max * Game.Settings.potion_Refresh_HealPersent);
			player.manaPoints.Current += (int)Math.Round(player.manaPoints.Max * Game.Settings.potion_Refresh_ManaPersent);
			return true;
		}
	}
}
