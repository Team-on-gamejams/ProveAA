using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Item.Weapon {
	abstract class BasicWeapon : BasicItem {
		public short dmgMod;

		public BasicWeapon() {
			itemImgPath += @"weapon\";
		}

		public override bool CardUsed(Player player) {
			player.EquipWeapon(this);
			return true;
		}

		public Uri GetOnPlayerItemImage() =>
			new Uri(Environment.CurrentDirectory + '\\' + itemImgPath + "Player.png", UriKind.Absolute);
	}
}
