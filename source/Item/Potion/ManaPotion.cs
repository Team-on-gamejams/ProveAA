using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Potion {
	class ManaPotion : BasicPotion {
		public override void UseCard(Player creature) {
			creature.manaPoints.current += (byte)Math.Round(creature.manaPoints.Max * Game.Settings.potion_Mana_ManaPersent);
		}
	}
}
