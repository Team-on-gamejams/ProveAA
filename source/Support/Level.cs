using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	class Level {
		public double CurrentExp, ExpToNext, expMod;
		public byte CurrentLvl, freePoints;

		public void GetExp(byte exp, double enemyExpMod) {
			CurrentExp += exp * enemyExpMod * expMod;
		}

		public byte GetPersent() {
			byte persent = (byte)Math.Round(CurrentExp * 100 / Math.Floor(ExpToNext));
			return persent == 0 ? (byte)(1) : persent;
		}

		public override string ToString() {
			return $"{CurrentExp} / {Math.Floor(ExpToNext)}  ({CurrentLvl})";
		}
	}
}
