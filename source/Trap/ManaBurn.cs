using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Trap {
	class ManaBurn : BasicTrap {
		public override bool PlayerStepIn(Player player) {
			PrintText("Mana burn!");
			player.manaPoints.Current -= player.manaPoints.Current / 4;
			return true;
		}
	}
}
