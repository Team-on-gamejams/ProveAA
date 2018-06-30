using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class ManaPotion : BasicPotion {
		public override void CardUsed(Creature.Player player) {
			player.manaPoints.Current += (byte)(player.hitPoints.Max * Game.Settings.potion_Mana_ManaPersent);
		}
	}
}
