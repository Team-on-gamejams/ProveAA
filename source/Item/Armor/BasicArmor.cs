using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Armor {
	abstract class BasicArmor : BasicItem {
		public byte armorMod;

		public BasicArmor() {
			itemImgPath += @"armor\";
		}

		public override bool CardUsed(Creature.Player player) {
			player.EquipArmor(this);
			return true;
		}

		public Uri GetOnPlayerItemImage() =>
			new Uri(Environment.CurrentDirectory + '\\' + itemImgPath + "Player.png", UriKind.Absolute);
	}
}
