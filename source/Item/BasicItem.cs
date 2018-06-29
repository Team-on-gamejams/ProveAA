using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item {
	abstract class BasicItem : Interface.ICardContent {
		public string name;

		public abstract void UseCard(Player pl);
	}
}
