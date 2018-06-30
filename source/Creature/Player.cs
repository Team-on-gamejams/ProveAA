using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveAA.Interface;
using ProveAA.Support;
using ProveAA.Game;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProveAA.Map;
using ProveAA.Card;

namespace ProveAA.Creature {
	class Player : BasicCreature {
		byte posX, posY;
		public Windows.GameWindow window;
		Map.GameMap map;
		List<Card.Card> cards;

		public readonly Level level;
		public byte PosX { get => posX; set { posX = value; } }
		public byte PosY { get => posY; set { posY = value; } }

		internal GameMap Map { get => map; set => map = value; }
		internal List<Card.Card> Cards { get => cards; set => cards = value; }

		public Player() {
			Cards = new List<Card.Card>(7);
			(new Card.Card(new Spell.Move.ExploreZone())).AddToHand(this);
			level = new Level();
			this.imageGridLeftTopCorner.Children.Add(
				new Image() { Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\player\player.png", UriKind.Absolute)) }
			);
			this.imageGridMaze.Children.Add(
				new Image() { Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\player\player.png", UriKind.Absolute)) }
			);
			Grid.SetZIndex(imageGridLeftTopCorner, 2);
			Grid.SetZIndex(imageGridMaze, 2);

			this.armor.Base = this.armor.Current = Settings.player_init_armor;
			this.attack.Base = this.attack.Current = Settings.player_init_attack;

			this.hitPoints.Max = Settings.player_init_hpMax;
			this.manaPoints.Max = Settings.player_init_mpMax;
			this.hitPoints.Current = Settings.player_init_hp;
			this.manaPoints.Current = Settings.player_init_mp;

			this.level.CurrentLvl = Settings.player_init_lvl;
			this.level.CurrentExp = 0;
			this.level.ExpToNext = Settings.player_init_toNextLvl;
		}

		public void InitOutput(Windows.GameWindow window, Map.GameMap map) {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				this.window = window;
				this.Map = map;
				window.LeftTopPlayerImage.Children.Add(imageGridLeftTopCorner);
				window.MazeGrid.Children.Add(imageGridMaze);
			});
		}

		public void TryLevelUp() {
			if (level.CurrentExp >= level.ExpToNext) {
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

		public void OutputPlayerInfo(Windows.GameWindow window) {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				window.HealbarText.Text = hitPoints.ToString();
				System.Windows.Controls.Grid.SetColumnSpan(window.HealbarRectangle, hitPoints.GetPersent());
				window.ManabarText.Text = manaPoints.ToString();
				System.Windows.Controls.Grid.SetColumnSpan(window.ManabarRectangle, manaPoints.GetPersent());

				//window.WeaponText.Text = "Оружие: ";
				//if (player.UsedWeapon != null)
				//	window.WeaponText.Text += player.UsedWeapon.ToString();
				//else
				//	window.WeaponText.Text += "------";
				//window.WeaponText.Text += $" ({player.attack.Current})";

				//window.ArmorText.Text = "Броня: ";
				//if (player.UsedArmor != null)
				//	window.ArmorText.Text += player.UsedArmor.ToString();
				//else
				//	window.ArmorText.Text += "------";
				//window.ArmorText.Text += $" ({player.armor.Current})";
			});
		}

		public void PosChanged() {
			Grid.SetRow(imageGridMaze, posY);
			Grid.SetColumn(imageGridMaze, posX);
			Map[posY, posX].IsVisited = true;
			for (byte i = (byte)(posY - 1); i <= posY + 1; ++i)
				for (byte j = (byte)(posX - 1); j <= posX + 1; ++j)
					Map[i, j].IsInFog = false;
			PlayerStepInCell(Map[posY, posX]);
		}

		public void PlayerStepInCell(Map.GameCell cell) {
			cell.cellContent?.PlayerStepIn(this);
			cell.cellContent = null;
		}
	}
}
