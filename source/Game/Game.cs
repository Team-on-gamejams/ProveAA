using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ProveAA.Map;
using ProveAA.Creature;

namespace ProveAA.Game {
	class Game {
		Windows.GameWindow window;
		Player player;
		Map.GameMap map;

		internal GameMap Map { get => map; set => map = value; }

		public void Start() {
			this.window = Singletones.gameWindow;
			player = new Player();

			GameMap.CreateMap(player);
		}

		void DisplayPlayerInfo() {
			window.HealbarText.Text = player.hitPoints.ToString();
			System.Windows.Controls.Grid.SetColumnSpan(window.HealbarRectangle, player.hitPoints.GetPersent());
			window.ManabarText.Text = player.manaPoints.ToString();
			System.Windows.Controls.Grid.SetColumnSpan(window.ManabarRectangle, player.hitPoints.GetPersent());

			window.WeaponText.Text = "Оружие: ";
			if (player.UsedWeapon != null)
				window.WeaponText.Text += player.UsedWeapon.ToString();
			else
				window.WeaponText.Text += "------";
			window.WeaponText.Text += $" ({player.attack.Current})";

			window.ArmorText.Text = "Броня: ";
			if (player.UsedArmor != null)
				window.ArmorText.Text += player.UsedArmor.ToString();
			else
				window.ArmorText.Text += "------";
			window.ArmorText.Text += $" ({player.armor.Current})";
		}
	}
}
