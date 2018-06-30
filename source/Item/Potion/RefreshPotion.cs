using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class RefreshPotion : BasicPotion {
		public override void CardUsed(Creature.Player player) {
			player.hitPoints.Current += (byte)(player.hitPoints.Max * Game.Settings.potion_Refresh_HealPersent);
			player.manaPoints.Current += (byte)(player.hitPoints.Max * Game.Settings.potion_Refresh_ManaPersent);
		}
	}
}
