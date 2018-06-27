using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA {
	static class Singletones {
		public static Game game;

		static Singletones() {
			game = new Game();
		}
	}
}
