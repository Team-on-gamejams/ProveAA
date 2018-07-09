using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Spell.Attack {
	class HolyWater : BasicSpell, Attributes.GhostKiller {
		public HolyWater() {
			this.itemImgPath += "HolyWater";
		}

		public override bool CardUsed(Player player) {
			player.manaPoints.Current = 0;
			player.hitPoints.Current = player.hitPoints.Max;
			if (player.IsInBattle && player.Enemy is Attributes.Undead) {
				player.Enemy.GetSpellAttack(10, this);
			}
			return true;
		}
	}
}
