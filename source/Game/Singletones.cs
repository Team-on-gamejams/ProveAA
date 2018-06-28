using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Game {
	static class Singletones {
		public static Random rand;
		public static Game game;

		static Singletones() {
			game = new Game();
			rand = new Random();
		}
	}
}
