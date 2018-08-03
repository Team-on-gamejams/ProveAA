using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Spell.Move {
	class EscapeFromLvl : OpenDoor {
		public EscapeFromLvl(byte _doorZoneStyleNum) : base(0, _doorZoneStyleNum) {
			itemImgPath = @"img\map\" + DoorZoneStyleNum.ToString() + @"\openDoor_" + DoorId.ToString();
		}

		public override bool CardUsed(Player pl) {
			if (pl.IsInBattle)
				return false;

			doorPos = FindDoor(pl);

			if (doorPos != null) {
				RecreateLvl(pl);
				return true;
			}

			return false;
		}

	}
}
