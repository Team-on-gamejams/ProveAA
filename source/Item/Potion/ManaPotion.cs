using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class ManaPotion : BasicPotion {
		public ManaPotion() {
			this.itemImgPath += "Mana";
			this.itemName = "Mana potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.manaPoints.Current += (byte)(player.manaPoints.Max * Game.Settings.potion_Mana_ManaPersent);
			return true;
		}
	}
}
