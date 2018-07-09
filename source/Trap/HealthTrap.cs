using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Trap {
	class HealthTrap : BasicTrap {
		public override bool PlayerStepIn(Creature.Player player) {
			PrintText("HP trap!");
			player.hitPoints.Current -= (byte)(player.hitPoints.Current / 4);
			return true;
		}
	}
}
