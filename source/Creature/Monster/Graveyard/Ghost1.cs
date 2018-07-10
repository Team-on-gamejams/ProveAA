using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Creature.Monster.Graveyard {
	class Ghost1 : BasicGraveyardMonster, Attributes.Ghost {
		public Ghost1(Creature.Player player) {
			this.monsterDifficult = 0;
			monsterImgPath += @"Ghost1";
			monsterName = "Ghost junior";

			//this.manaPoints.Max = 9;
			//this.manaPoints.Current = 9;

			//this.hitPoints.Max = 9;
			//this.hitPoints.Current = 9;

			//this.armor.Base = 2;
			//this.armor.Current = 2;

			//this.attack.Base = 2;
			//this.attack.Current = 2;
			this.BalanceMonster(player);
		}
	}
}
