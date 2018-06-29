using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	class Bar {
		public byte current;
		public byte Current { set { current = value; if (current > Max) current = Max; } get => current; }
		public byte Max { set; get; }

		public Byte GetPersent() => (byte)Math.Round(Current * 100f / Max);

		public override string ToString() {
			return $"{Current} / {Max}";
		}
	}
}
