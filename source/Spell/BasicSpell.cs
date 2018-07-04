using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Spell {
	abstract class BasicSpell : Interface.ICardContent {
		public string itemImgPath;

		public BasicSpell() {
			itemImgPath = @"img\spell\";
		}

		public abstract bool CardUsed(Player player);

		public Uri GetImageForCard() =>
			new Uri(Environment.CurrentDirectory + '\\' + itemImgPath + "Card.png", UriKind.Absolute);

		public Uri GetImageForCell() =>
			new Uri(Environment.CurrentDirectory + '\\' + itemImgPath + "Cell.png", UriKind.Absolute);
	}
}
