using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Item.Potion {
	abstract class BasicPotion : BasicItem {
		public BasicPotion() {
			itemImgPath += @"potion\";
		}
	}
}
