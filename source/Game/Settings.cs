using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Game {
	static class Settings {
		public static byte handSize = 12;
		public static byte map_sizeX = 15;
		public static byte map_sizeY = 15;

		public static byte player_init_hp = 30;
		public static byte player_init_mp = 30;
		public static byte player_init_hpMax = 30;
		public static byte player_init_mpMax = 30;
		public static byte player_init_armor = 1;
		public static byte player_init_attack = 1;

		public static byte player_init_lvl = 1;
		public static byte player_init_toNextLvl = 100;
		public static float player_lvl_expModFromGet = 1f;
		public static float player_lvl_expModFromGetAdditional = 0.20f;
		public static float player_lvl_expModToNextLevel = 1.2f;
		public static byte player_lvl_addToMaxHp = 10;
		public static byte player_lvl_addToMaxMp = 5;
		public static byte player_lvl_addToMaxHpAdditional = 10;
		public static byte player_lvl_addToMaxMpAdditional = 10;
		public static byte player_lvl_addToArmor = 1;
		public static byte player_lvl_addToAttack = 1;
		public static bool player_lvl_refreshHp = true;
		public static bool player_lvl_refreshMp = true;

		public static float ApplyBonusEachNLvl = 3;
		public static float ExpPenaltyPerLevelBelow = 0.25f;
		public static float ExpBonusPerLevelAbove = 0.25f;

		public static float Enemy_Mult_WeaponStat = 0.5f;
		public static float Enemy_Mult_ArmorStat = 0.5f;

		public static int Enemy_Lvl_BasicExp = 100;
		public static int Enemy_Lvl_BonusExpPerLvl = 10;
		public static List<double> Enemy_Lvl_HpDiv = new List<double>() {   4, 2, 1.2, 1, 0.8, 0.5 };
		public static List<double> Enemy_Lvl_expMod = new List<double>(){ 0.5, 1, 2,   3, 4,   6 };

		public static float potion_Healing_HealPersent = 0.25f;
		public static float potion_Mana_ManaPersent = 0.25f;
		public static float potion_Refresh_HealPersent = 0.50f;
		public static float potion_Refresh_ManaPersent = 0.50f;

		public static bool mazeGen_PlaceFog = false;
		public static bool mazeGen_StartOnCenter = true;
		public static List<ushort> mazeGen_crossOnItterCnt = new List<ushort>() {0, 3};
	}
}
