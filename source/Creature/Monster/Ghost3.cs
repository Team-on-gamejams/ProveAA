using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Creature.Monster {
	class Ghost3 : BasicMonster, Attributes.BasicGhost {
		public Ghost3(Creature.Player player) {
			monsterDifficult = MonsterDifficult.lvl3;
			monsterImgPath += @"Ghost3";
			monsterName = "Ghost senior";

			//this.manaPoints.Max = 28;
			//this.manaPoints.Current = 28;

			//this.hitPoints.Max = 28;
			//this.hitPoints.Current = 28;

			//this.armor.Base = 6;
			//this.armor.Current = 6;

			//this.attack.Base = 6;
			//this.attack.Current = 6;
			this.BalanceMonster(player);
		}
	}
}
