using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	class Bar {
		public byte current;
		private byte _max;

		public byte Current { set { current = value; if (current > Max) current = Max; Changed?.Invoke(); } get => current; }
		public byte Max { get => _max; set { _max = value; Changed?.Invoke(); } }

		public Byte GetPersent() => (byte)Math.Round(Current * 100f / Max);

		public override string ToString() {
			return $"{Current} / {Max}";
		}

		public delegate void ValueChanged();
		public event ValueChanged Changed;
	}
}
