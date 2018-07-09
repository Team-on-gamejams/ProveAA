using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	class Level {
		public double CurrentExp, freePoints;
		public double ExpToNext;
		public byte CurrentLvl;

		public override string ToString() {
			return $"{CurrentExp} / {(int)ExpToNext}  ({CurrentLvl})";
		}
	}
}
