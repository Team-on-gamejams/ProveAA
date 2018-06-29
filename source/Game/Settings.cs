using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Game {
	static class Settings {
		public static byte map_sizeX = 20;
		public static byte map_sizeY = 20;

		public static byte player_init_hp = 10;
		public static byte player_init_mp = 10;
		public static byte player_init_hpMax = 10;
		public static byte player_init_mpMax = 10;
		public static byte player_init_armor = 0;
		public static byte player_init_attack = 1;

		public static byte player_init_lvl = 1;
		public static byte player_init_toNextLvl = 5;
		public static float player_lvl_expMod = 1.2f;
		public static byte player_lvl_addToMaxHp = 1;
		public static byte player_lvl_addToMaxMp = 1;
		public static byte player_lvl_addToArmor = 0;
		public static byte player_lvl_addToAttack = 0;
		public static bool player_lvl_refreshHp = true;
		public static bool player_lvl_refreshMp = true;

		public static float potion_Heal_HealPersent = 0.25f;
		public static float potion_Mana_ManaPersent = 0.25f;
		public static float potion_Refresh_HealPersent = 0.50f;
		public static float potion_Refresh_ManaPersent = 0.50f;
	}
}
