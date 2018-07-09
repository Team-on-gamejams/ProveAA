using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Creature.Monster {
	class Ghost2 : BasicMonster, Attributes.BasicGhost {
		public Ghost2(Creature.Player player) {
			monsterDifficult = MonsterDifficult.lvl2;
			monsterImgPath += @"Ghost2";
			monsterName = "Ghost middle";

			//this.manaPoints.Max = 15;
			//this.manaPoints.Current = 15;

			//this.hitPoints.Max = 15;
			//this.hitPoints.Current = 15;

			//this.armor.Base = 4;
			//this.armor.Current = 4;

			//this.attack.Base = 4;
			//this.attack.Current = 4;
			this.BalanceMonster(player);
		}
	}
}
