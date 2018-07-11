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
		public int expFromEnemy;

		public string monsterName;
		public string monsterImgPath;

		public BasicMonster() {
			monsterImgPath = @"img\monster\";
		}

		protected void BalanceMonster(Creature.Player player) {
			this.expFromEnemy =(int)Math.Round(Game.Settings.Enemy_Lvl_expMod[monsterDifficult] * Game.Settings.Enemy_Lvl_BasicExp);
			this.hitPoints.Max = (int)Math.Round(player.hitPoints.Max / Game.Settings.Enemy_Lvl_HpDiv[monsterDifficult]);
			this.hitPoints.Current = this.hitPoints.Max;

			ushort monsterLevel = player.level.CurrentLvl;
			if(player.level.CurrentLvl < minMonsterLevel || player.level.CurrentLvl > maxMonsterLevel) 
				monsterLevel = maxMonsterLevel;

			expFromEnemy += Game.Settings.Enemy_Lvl_BonusExpPerLvl * (monsterLevel - 1);
			if (monsterLevel != player.level.CurrentLvl) {
				short diff = (short)((monsterLevel - player.level.CurrentLvl) / 3);
				if(diff > 0) {
					while(diff-- != 0)
						expFromEnemy = (int)Math.Round(expFromEnemy * Game.Settings.ExpBonusPerLevelAbove);
				}
				else if(diff < 0) {
					while (diff++ != 0)
						expFromEnemy = (int)Math.Round(expFromEnemy * Game.Settings.ExpPenaltyPerLevelBelow);
				}
				if (expFromEnemy < 0)
					expFromEnemy = 0;
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
