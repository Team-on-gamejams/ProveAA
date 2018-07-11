using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProveAA.Creature.Monster {
	abstract class BasicMonster : BasicCreature, Interface.ICellContent {
		protected byte statChanceAttack = 50;

		protected ushort minMonsterLevel;
		protected ushort maxMonsterLevel;
		protected byte monsterDifficult;
		public double expMod;

		public string monsterName;
		public string monsterImgPath;

		public BasicMonster() {
			monsterImgPath = @"img\monster\";
		}

		protected void BalanceMonster(Creature.Player player) {
			this.expMod = Game.Settings.Enemy_Lvl_expMod[monsterDifficult];
			this.hitPoints.Max = (int)Math.Round(player.hitPoints.Max / Game.Settings.Enemy_Lvl_HpDiv[monsterDifficult]);
			this.hitPoints.Current = this.hitPoints.Max;

			ushort monsterLevel = player.level.CurrentLvl;
			if(player.level.CurrentLvl < minMonsterLevel || player.level.CurrentLvl > maxMonsterLevel) 
				monsterLevel = maxMonsterLevel;

			if(monsterLevel != player.level.CurrentLvl) {
				short diff = (short)((monsterLevel - player.level.CurrentLvl) / 3);
				if(diff > 0) {
					while(diff-- != 0)
						expMod +=  Game.Settings.ExpBonusPerLevelAbove;
				}
				else if(diff < 0) {
					while (diff++ != 0)
						expMod -= Game.Settings.ExpPenaltyPerLevelBelow;
				}
				if (expMod < 0)
					expMod = 0;
			}

			ushort maxStat = (ushort)(Game.Settings.player_init_armor + Game.Settings.player_init_attack - 1 + 
				monsterLevel - 1 + 
				Math.Round((player.UsedArmor?.armorMod ?? 0) * Game.Settings.Enemy_Mult_ArmorStat +
				(player.UsedWeapon?.dmgMod ?? 0) * Game.Settings.Enemy_Mult_WeaponStat)
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
