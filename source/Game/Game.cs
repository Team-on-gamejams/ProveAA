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
		BasicCreature player;

		public Game() {
			map = new Map.GameMap();
			player = new BasicCreature();
		}

		public void Start(Windows.GameWindow window) {
			map.NewLevel(player);
		}
	}
}
