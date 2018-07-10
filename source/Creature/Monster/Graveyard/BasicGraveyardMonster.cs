using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Creature.Monster.Graveyard {
	class BasicGraveyardMonster : BasicMonster, ProveAA.Attributes.Undead {
		public BasicGraveyardMonster() {
			statChanceAttack = 50;
			minMonsterLevel = 1;
			maxMonsterLevel = 5;
			monsterDifficult = 0;
		}	
	}
}
