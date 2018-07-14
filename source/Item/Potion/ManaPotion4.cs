﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	class ManaPotion4 : BasicPotion {
		public ManaPotion4() {
			this.itemImgPath += "Mana4";
			this.itemName = "Mana potion";
		}

		public override bool CardUsed(Creature.Player player) {
			player.manaPoints.Current += (int)Math.Round(player.manaPoints.Max * 0.80);
			return true;
		}
	}
}
