using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Creature.Monster {
	abstract class BasicMonster : BasicCreature, Interface.ICellContent {
		protected byte statChanceAttack = 50;

		protected byte minMonsterLevel;
		protected byte maxMonsterLevel;
		protected byte monsterDifficult;
		public double expMod = 1;

		public string monsterName;
		public string monsterImgPath;

		public BasicMonster() {
			monsterImgPath = @"img\monster\";
		}

		protected void BalanceMonster(Creature.Player player) {
			this.expMod = Game.Settings.Enemy_Lvl_expMod[monsterDifficult];
			this.hitPoints.Max = (byte)(player.hitPoints.Max / Game.Settings.Enemy_Lvl_HpDiv[monsterDifficult]);
			this.hitPoints.Current = this.hitPoints.Max;

			ushort monsterLevel = player.level.CurrentLvl;
			if(player.level.CurrentLvl < minMonsterLevel || player.level.CurrentLvl > maxMonsterLevel) 
				monsterLevel = maxMonsterLevel;

			ushort maxStat = (ushort)(Game.Settings.player_init_armor + Game.Settings.player_init_attack - 1 + 
				monsterLevel - 1 + 
				(player.UsedArmor?.armorMod ?? 0) +
				(player.UsedWeapon?.dmgMod ?? 0)
			);
			this.attack.Current = 1;
			this.armor.Current = 0;
			while (maxStat-- != 0) {
				if (Game.Rand.NextPersent() < this.statChanceAttack)
					++this.attack.Current;
				else
					++this.armor.Current;
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
