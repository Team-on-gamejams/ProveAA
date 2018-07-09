using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Creature.Monster {
	abstract class BasicMonster : BasicCreature, Interface.ICellContent {
		protected enum MonsterDifficult : byte { lvl1, lvl2, lvl3 };

		protected MonsterDifficult monsterDifficult;
		public string monsterName;
		public string monsterImgPath;

		public BasicMonster() {
			monsterImgPath = @"img\monster\";
		}

		protected void BalanceMonster(Creature.Player player) {
			this.hitPoints.Max = player.hitPoints.Max;
			switch (monsterDifficult) {
			case MonsterDifficult.lvl1:
			this.hitPoints.Max = (byte)(this.hitPoints.Max / Game.Settings.Enemy_Lvl1_HpDiv);
			break;
			case MonsterDifficult.lvl2:
			this.hitPoints.Max = (byte)(this.hitPoints.Max / Game.Settings.Enemy_Lvl2_HpDiv);
			break;
			case MonsterDifficult.lvl3:
			this.hitPoints.Max = (byte)(this.hitPoints.Max / Game.Settings.Enemy_Lvl3_HpDiv);
			break;
			}
			this.hitPoints.Current = player.hitPoints.Max;

			ushort maxStat = (ushort)(player.attack.Current + player.armor.Current - 1);
			this.attack.Current = 1;
			this.armor.Current = 0;
			while (maxStat-- != 0) {
				if (Game.Rand.Next(0, 2) == 1)
					++this.armor.Current;
				else
					++this.attack.Current;
			}
		}

		public Uri GetDisplayImage() =>
			new Uri(Environment.CurrentDirectory + '\\' + monsterImgPath + ".png", UriKind.Absolute);

		public Uri GetBattleImage() =>
			new Uri(Environment.CurrentDirectory + '\\' + monsterImgPath + "Battle.png", UriKind.Absolute);

		public bool PlayerStepIn(Player player) {
			player.StartBattle(this);
			return true;
		}
	}
}
