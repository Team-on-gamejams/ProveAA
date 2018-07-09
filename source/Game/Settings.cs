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
		public static byte player_init_toNextLvl = 1;
		public static float player_lvl_expMod = 1.2f;
		public static byte player_lvl_addToMaxHp = 10;
		public static byte player_lvl_addToMaxMp = 5;
		public static byte player_lvl_addToArmor = 1;
		public static byte player_lvl_addToAttack = 1;
		public static bool player_lvl_refreshHp = true;
		public static bool player_lvl_refreshMp = true;

		public static float Enemy_Lvl1_HpDiv = 4f;
		public static float Enemy_Lvl2_HpDiv = 2f;
		public static float Enemy_Lvl3_HpDiv = 1.2f;


		public static float potion_Healing_HealPersent = 0.25f;
		public static float potion_Mana_ManaPersent = 0.25f;
		public static float potion_Refresh_HealPersent = 0.50f;
		public static float potion_Refresh_ManaPersent = 0.50f;

		public static bool mazeGen_PlaceFog = false;
		public static bool mazeGen_StartOnCenter = true;
		public static List<ushort> mazeGen_crossOnItterCnt = new List<ushort>() {0, 3};
	}
}
