using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Potion {
	class RefreshPotion : BasicPotion {
		public override void UseCard(Player creature) {
			creature.hitPoints.current += (byte)Math.Round(creature.hitPoints.Max * Game.Settings.potion_Refresh_HealPersent);
			creature.manaPoints.current += (byte)Math.Round(creature.manaPoints.Max * Game.Settings.potion_Refresh_ManaPersent);
		}
	}
}
