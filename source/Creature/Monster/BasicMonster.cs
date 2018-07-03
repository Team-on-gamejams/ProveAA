using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Creature.Monster {
	abstract class BasicMonster : BasicCreature, Interface.ICellContent {
		public string monsterName;
		public string monsterImgPath;

		public BasicMonster() {
			monsterImgPath = @"img\monster\";
		}

		public Uri GetDisplayImage() =>
			new Uri(Environment.CurrentDirectory + '\\' + monsterImgPath + ".png", UriKind.Absolute);


		public void PlayerStepIn(Player player) {
			player.StartBattle(this);
		}
	}
}
