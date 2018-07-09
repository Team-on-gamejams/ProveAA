using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Spell.Attack {
	class Fireball : BasicSpell {
		public Fireball() {
			this.itemImgPath += "Fireball";
		}

		public override bool CardUsed(Player player) {
			if (!player.IsInBattle)
				return false;

			player.manaPoints.Current -= 2;
			player.Enemy.GetDmgWithArmor(20);

			return true;
		}
	}
}
