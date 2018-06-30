using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item {
	abstract class BasicItem : Interface.ICardContent {
		public abstract void CardUsed(Player player);
		public abstract Uri GetImageForCard();
		public abstract Uri GetImageForCell();
	}
}
