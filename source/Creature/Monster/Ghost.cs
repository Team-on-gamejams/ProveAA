using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Creature.Monster {
	class Ghost : BasicMonster {
		public Ghost() {
			monsterImgPath += @"Ghost";
			monsterName = "Ghost";

			this.manaPoints.Max = 10;
			this.manaPoints.Current = 10;

			this.hitPoints.Max = 50;
			this.hitPoints.Current = 50;

			this.armor.Base = 0;
			this.armor.Current = 0;

			this.attack.Base = 0;
			this.attack.Current = 1;
		}
	}
}
