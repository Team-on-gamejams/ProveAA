using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Game {
	static class Singletones {
		public static Random rand;
		public static Game game;
		public static Creature.Player player;
		public static Windows.GameWindow gameWindow;

		static Singletones() {
			rand = new Random();
			game = new Game();
			gameWindow = new Windows.GameWindow();
		}
	}
}
