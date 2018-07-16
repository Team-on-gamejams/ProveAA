using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	class Level {
		public uint CurrentExp, ExpToNext;
		public double expMod;
		public ushort CurrentLvl, freePoints;

		public void GetExp(int exp) {
			CurrentExp += (uint)Math.Round(exp * expMod);
		}

		public byte GetPersent() {
			byte persent = (byte)Math.Round(CurrentExp * 100f / ExpToNext);
			return persent == 0 ? (byte)(1) : persent;
		}

		public override string ToString() {
			return $"{CurrentExp} / {ExpToNext}  ({CurrentLvl}, {expMod:F2})";
		}
	}
}
