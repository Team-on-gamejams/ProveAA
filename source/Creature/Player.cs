using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveAA.Card;
using ProveAA.Item;
using ProveAA.Item.Armor;
using ProveAA.Item.Weapon;

using ProveAA.Game;

namespace ProveAA.Creature {
	class Player : BasicCreature {
		public byte posX, posY;

		BasicCard[] cards;
		BasicWeapon usedWeapon;
		BasicArmor usedArmor;

		public Player() {
			cards = new BasicCard[7];

			this.armor.Base = this.armor.Current = Settings.player_init_armor;
			this.attack.Base = this.attack.Current = Settings.player_init_attack;

			this.hitPoints.Current = Settings.player_init_hp;
			this.manaPoints.Current = Settings.player_init_mp;
			this.hitPoints.Max = Settings.player_init_hpMax;
			this.manaPoints.Max = Settings.player_init_mpMax;

			this.level.CurrentLvl = Settings.player_init_lvl;
			this.level.CurrentExp = 0;
			this.level.ExpToNext = Settings.player_init_toNextLvl;
		}

		public void TryLevelUp() {
			if(level.CurrentExp >= level.ExpToNext) {
				++level.CurrentLvl;
				level.CurrentExp -= level.ExpToNext;
				level.ExpToNext = (byte)Math.Round(level.ExpToNext * Settings.player_lvl_expMod);

				hitPoints.Max += Settings.player_lvl_addToMaxHp;
				manaPoints.Max += Settings.player_lvl_addToMaxMp;

				armor.Base += Settings.player_lvl_addToArmor;
				armor.Current += Settings.player_lvl_addToArmor;

				attack.Base += Settings.player_lvl_addToAttack;
				attack.Current += Settings.player_lvl_addToAttack;

				if (Settings.player_lvl_refreshHp)
					hitPoints.Current = hitPoints.Max;
				if (Settings.player_lvl_refreshMp)
					manaPoints.Current = manaPoints.Max;
			}
		}

	}
}
