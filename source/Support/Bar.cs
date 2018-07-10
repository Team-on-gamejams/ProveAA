using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	class Bar {
		private byte current;
		private byte _max;

		public byte Current { set { current = value; if (current > Max) current = Max; Changed?.Invoke(); } get => current; }
		public byte Max { get => _max; set { _max = value; if (Max < Current) Current = Max; Changed?.Invoke(); } }

		public Byte GetPersent() {
			byte persent = (byte)Math.Round(Current * 100f / Max);
			return persent == 0 ? (byte)(1) : persent;
		}

		public void AddToBoth(byte num) {
			_max += num;
			current += num;
		}

		public override string ToString() {
			return $"{Current} / {Max}";
		}

		public delegate void ValueChanged();
		public event ValueChanged Changed;
	}
}
