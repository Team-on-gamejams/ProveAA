using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item {
	abstract class BasicItem : Interface.ICardContent { 
		public string itemName;
		public string itemImgPath;

		public BasicItem() {
			itemImgPath = @"\img\";
		}

		public abstract bool CardUsed(Player player);

		public Uri GetImageForCard() =>
			new Uri(Environment.CurrentDirectory + '\\' + itemImgPath + "Card.png" , UriKind.Absolute);

		public Uri GetImageForCell() =>
			new Uri(Environment.CurrentDirectory + '\\' + itemImgPath + "Cell.png", UriKind.Absolute);
	}
}
