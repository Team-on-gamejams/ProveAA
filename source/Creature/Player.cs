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
		private byte posY;
		private byte posX;

		public BasicCard[] Cards { get; private set; }
		public BasicWeapon UsedWeapon { get; private set; }
		public BasicArmor UsedArmor { get; private set; }
		public byte PosX { get => posX; set{ posX = value; System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate { System.Windows.Controls.Grid.SetColumn(image, PosX); RemoveFogFromNearbyCells(); }); } }
		public byte PosY { get => posY; set{ posY = value; System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate { System.Windows.Controls.Grid.SetRow(image, PosY); RemoveFogFromNearbyCells(); }); } }

		public Player() {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				image.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\player\player.png", UriKind.Absolute));
				Singletones.gameWindow.MazeGrid.Children.Add(image);
				Singletones.gameWindow.LeftTopPlayerImage.Source = image.Source;
			});
			Cards = new BasicCard[7];

			this.armor.Base = this.armor.Current = Settings.player_init_armor;
			this.attack.Base = this.attack.Current = Settings.player_init_attack;

			this.hitPoints.Current = Settings.player_init_hp;
			this.manaPoints.Current = Settings.player_init_mp;
			this.hitPoints.Max = Settings.player_init_hpMax;
			this.manaPoints.Max = Settings.player_init_mpMax;

			this.level.CurrentLvl = Settings.player_init_lvl;
			this.level.CurrentExp = 0;
			this.level.ExpToNext = Settings.player_init_toNextLvl;

			posX = posY = 1;
			Singletones.player = this;
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

		public void UseCard(BasicCard item) {
			item.cardContent.UseCard(this);
		}

		public void RecalcStats() {
			armor.Current = (byte)(armor.Base + UsedArmor.armorBonus);
			attack.Current = (byte)(attack.Base + UsedWeapon.attackBonus);
		}

		void RemoveFogFromNearbyCells() {
			for (byte y = (byte)(posY - 1); y < posY + 1; ++y)
				for (byte x = (byte)(posX - 1); x < posX + 1; ++x)
					if(Game.Singletones.game.Map != null)
						Game.Singletones.game.Map[y, x].IsInFog = false;
		}
	}
}
