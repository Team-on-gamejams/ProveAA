using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Spell.Attack {
	class HolyWater : BasicSpell {
		public HolyWater() {
			this.itemImgPath += "HolyWater";
		}

		public override bool CardUsed(Player player) {
			player.hitPoints.Current = player.hitPoints.Max;
			if (player.IsInBattle && player.Enemy is Attributes.BasicGhost) {
				player.Enemy.GetSpellAttack(10, this);
			}
			return true;
		}
	}
}
