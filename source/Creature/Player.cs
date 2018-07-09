﻿using System;
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
using ProveAA.Item.Armor;
using ProveAA.Item.Weapon;

namespace ProveAA.Creature {
	class Player : BasicCreature {
		byte posX, posY;
		Map.GameMap map;
		List<Card.Card> cards;

		Image armorImage, weaponImage;
		public BasicArmor UsedArmor { get; private set; }
		public BasicWeapon UsedWeapon { get; private set; }

		public readonly Level level;
		public Windows.GameWindow window;
		public byte PosX { get => posX; set { posX = value; } }
		public byte PosY { get => posY; set {  posY = value; } }

		public bool IsInBattle { get; set; }
		public Creature.Monster.BasicMonster Enemy { get; set; }

		internal GameMap Map { get => map; set => map = value; }
		internal List<Card.Card> Cards { get => cards; set => cards = value; }

		public Player() {
			armorImage = new Image();
			weaponImage = new Image();
			Cards = new List<Card.Card>(Settings.handSize);
			//(new Card.Card(new Spell.Move.ExploreZone())).AddToHand(this);

			var playerImg = new Image() { Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\player\player.png", UriKind.Absolute)) };
			this.imageGridMaze.Children.Add(playerImg);
			this.imageGridMaze.Children.Add(weaponImage);
			this.imageGridMaze.Children.Add(armorImage);
			Grid.SetZIndex(imageGridMaze, 2);
			Grid.SetZIndex(playerImg, 2);
			Grid.SetZIndex(weaponImage, 3);
			Grid.SetZIndex(armorImage, 4);

			this.armor.Base = this.armor.Current = Settings.player_init_armor;
			this.attack.Base = this.attack.Current = Settings.player_init_attack;

			this.hitPoints.Max = Settings.player_init_hpMax;
			this.manaPoints.Max = Settings.player_init_mpMax;
			this.hitPoints.Current = Settings.player_init_hp;
			this.manaPoints.Current = Settings.player_init_mp;

			level = new Level();
			this.level.CurrentLvl = Settings.player_init_lvl;
			this.level.CurrentExp = 0;
			this.level.ExpToNext = Settings.player_init_toNextLvl;
		}

		public void InitOutput(Windows.GameWindow window, Map.GameMap map) {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				this.window = window;
				this.Map = map;
				window.MazeGrid.Children.Add(imageGridMaze);
				window.LeftTopPlayerImage.Children.Clear();
				window.LeftTopPlayerImage.Children.Add(CloneGrid(imageGridMaze));

				window.KeyDown += (a, b) => {
					if (!this.IsInBattle) {
						byte newX = PosX, newY = PosY;
						GameCell cell;

						if (b.Key == Key.Left || b.Key == Key.A) {
							--newX;
							TrySet();
						}
						else if (b.Key == Key.Right || b.Key == Key.D) {
							++newX;
							TrySet();
						}
						else if (b.Key == Key.Up || b.Key == Key.W) {
							--newY;
							TrySet();
						}
						else if (b.Key == Key.Down || b.Key == Key.S) {
							++newY;
							TrySet();
						}

						void TrySet() {
							cell = map[newY, newX];
							if (cell == null)
								return;
							if (!cell.IsSolid) {
								posX = newX;
								posY = newY;
								PosChanged();
								return;
							}
							if (cell.IsDoor) {
								for (int i = 0; i < this.Cards.Count; ++i) {
									if (Cards[i].CardContent is Spell.Move.OpenDoor card) {
										if (card.DoorId == map[newY, newX].DoorId) {
											Cards[i].Use(this);
											return;
										}
									}
								}
							}

						}
					}
				};

				window.WeaponText.MouseLeftButtonDown += (a, b) => {
					if(level.freePoints != 0) {
						--level.freePoints;
						attack.Base += Settings.player_lvl_addToAttack;
						attack.Current += Settings.player_lvl_addToAttack;
						OutputPlayerInfo();
					}
				};

				window.ArmorText.MouseLeftButtonDown += (a, b) => {
					if (level.freePoints != 0) {
						--level.freePoints;
						armor.Base += Settings.player_lvl_addToArmor;
						armor.Current += Settings.player_lvl_addToArmor;
						OutputPlayerInfo();
					}
				};

				ChangeToMaze();
			});
		}

		public void TryLevelUp() {
			if (level.CurrentExp >= Math.Floor(level.ExpToNext)) {
				++level.CurrentLvl;
				++level.freePoints;
				level.CurrentExp -= Math.Floor(level.ExpToNext);
				level.ExpToNext *= Settings.player_lvl_expMod;

				hitPoints.Max += Settings.player_lvl_addToMaxHp;
				manaPoints.Max += Settings.player_lvl_addToMaxMp;

				if (Settings.player_lvl_refreshHp)
					hitPoints.Current = hitPoints.Max;
				if (Settings.player_lvl_refreshMp)
					manaPoints.Current = manaPoints.Max;

				OutputPlayerInfo();
				TryLevelUp();
			}
		}

		public void OutputPlayerInfo() {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				window.HealbarText.Text = hitPoints.ToString();
				System.Windows.Controls.Grid.SetColumnSpan(window.HealbarRectangle, hitPoints.GetPersent());
				window.ManabarText.Text = manaPoints.ToString();
				System.Windows.Controls.Grid.SetColumnSpan(window.ManabarRectangle, manaPoints.GetPersent());
				SetWeaponText();
				SetArmorText();
			});
		}

		public void EquipArmor(BasicArmor newArmor) {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				if (UsedArmor != null)
					armor.Current -= UsedArmor.armorMod;
				UsedArmor = newArmor;
				if (UsedArmor != null) {
					armor.Current += UsedArmor.armorMod;
					armorImage.Source = new BitmapImage(UsedArmor.GetOnPlayerItemImage());
				}

				SetArmorText();
				window.LeftTopPlayerImage.Children.Clear();
				window.LeftTopPlayerImage.Children.Add(CloneGrid(imageGridMaze));
			});
		}
		void SetArmorText() {
			window.ArmorText.Text = "Armor: ";
			if (UsedArmor != null)
				window.ArmorText.Text += UsedArmor.itemName;
			else
				window.ArmorText.Text += "------";
			window.ArmorText.Text += $" ({armor.Current})";
			if (level.freePoints != 0)
				window.ArmorText.Text += " [+]";
		}

		public void EquipWeapon(BasicWeapon newWeapon) {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {

				if (UsedWeapon != null)
					attack.Current -= UsedWeapon.dmgMod;
				UsedWeapon = newWeapon;
				if (UsedWeapon != null) {
					attack.Current += UsedWeapon.dmgMod;
					weaponImage.Source = new BitmapImage(UsedWeapon.GetOnPlayerItemImage());
				}

				SetWeaponText();
				window.LeftTopPlayerImage.Children.Clear();
				window.LeftTopPlayerImage.Children.Add(CloneGrid(imageGridMaze));
			});
		}
		void SetWeaponText() {
			window.WeaponText.Text = "Weapon: ";
			if (UsedWeapon != null)
				window.WeaponText.Text += UsedWeapon.itemName;
			else
				window.WeaponText.Text += "------";
			window.WeaponText.Text += $" ({attack.Current})";
			if (level.freePoints != 0)
				window.WeaponText.Text += " [+]";
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
			if(cell.CellContent?.PlayerStepIn(this)??false)
				cell.CellContent = null;
		}

		public void StartBattle(Creature.Monster.BasicMonster monster) {
			IsInBattle = true;
			Enemy = monster;

			System.Timers.Timer battleTimer = new System.Timers.Timer() {
				AutoReset = false,
				Enabled = false,
				Interval = 230,
			};

			battleTimer.Elapsed += (sender, eventArgs) => {
				System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
					bool rez = this.GetAttack(Enemy) || Enemy.GetAttack(this);

					UpdateEnemy();

					if (rez) {
						battleTimer.Stop();
						IsInBattle = false;
						++level.CurrentExp;
						TryLevelUp();
						ChangeToMaze();
						return;
					}
					else
						battleTimer.Start();

				});
			};

			ChangeToBattle();
			battleTimer.Start();

		}

		void ChangeToBattle() {
			window.EnemyImage.Source = new BitmapImage(Enemy.GetBattleImage());

			ushort allStat = (ushort)(Enemy.attack.Current + Enemy.armor.Current);
			byte attackPersent = (byte)((Enemy.attack.Current * 100) / allStat);
			byte armorPersent = (byte)(100 - attackPersent);
			if (attackPersent == 0)
				attackPersent = 1;
			if (armorPersent == 0)
				armorPersent = 1;

			window.EnemyStatAttackText.Text = Enemy.attack.Current.ToString();
			Grid.SetColumnSpan(window.EnemyStatAttackViewbox, attackPersent);
			Grid.SetColumnSpan(window.EnemyStatAttackRectangle, attackPersent);

			window.EnemyStatArmorText.Text = Enemy.armor.Current.ToString();
			Grid.SetColumn(window.EnemyStatArmorViewbox, attackPersent);
			Grid.SetColumn(window.EnemyStatArmorRectangle, attackPersent);
			Grid.SetColumnSpan(window.EnemyStatArmorViewbox, armorPersent);
			Grid.SetColumnSpan(window.EnemyStatArmorRectangle, armorPersent);

			UpdateEnemy();

			window.MazeGrid.Opacity = 0.3;
			window.BattleGrid.Opacity = 1;
		}

		public void UpdateEnemy() {
			if (IsInBattle) {
				window.EnemyHpText.Text = Enemy.hitPoints.ToString();
				Grid.SetColumnSpan(window.EnemyHpRectangle, Enemy.hitPoints.GetPersent());
				window.PlayerHpText.Text = hitPoints.ToString();
				Grid.SetColumnSpan(window.PlayerHpRectangle, hitPoints.GetPersent());
			}
		}

		void ChangeToMaze() {
			window.MazeGrid.Opacity = 1;
			window.BattleGrid.Opacity = 0;
		}
	}
}
