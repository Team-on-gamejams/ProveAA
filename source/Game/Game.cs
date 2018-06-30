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

using ProveAA.Creature;

namespace ProveAA.Game {
	class Game {
		Map.GameMap map;
		Player player;

		public Game(Windows.GameWindow window) {
			map = new Map.GameMap(window);
			player = new Player();
			player.InitOutput(window, map);
		}

		public void Start(Windows.GameWindow window) {
			map.NewLevel(player);
			map.OutputMap(window);

			player.OutputPlayerInfo(window);
			player.hitPoints.Changed += ()=> player.OutputPlayerInfo(window);
			player.manaPoints.Changed += ()=> player.OutputPlayerInfo(window);
		}
	}
}
