using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Potion {
	class HealingPotion : BasicPotion {
		public override void CardUsed(Player player) {
			player.hitPoints.Current += (byte)(player.hitPoints.Max * Game.Settings.potion_Healing_HealPersent);
		}

		public override Uri GetImageForCard() =>
			new Uri(Environment.CurrentDirectory + @"\img\potion\HealingCard.png", UriKind.Absolute);

		public override Uri GetImageForCell() =>
			new Uri(Environment.CurrentDirectory + @"\img\potion\HealingCell.png", UriKind.Absolute);
	}
}
