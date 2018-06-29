using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Potion {
	class HealPotion : BasicPotion {
		public override void UseCard(Player creature) {
			creature.hitPoints.current += (byte)Math.Round(creature.hitPoints.Max * Game.Settings.potion_Heal_HealPersent);
		}
	}
}
