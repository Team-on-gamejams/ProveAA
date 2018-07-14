using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Spell.Move {
	class OpenDoor : BasicSpell {
		public byte DoorId { get; set; }
		public byte DoorZoneStyleNum { get; set; }

		public OpenDoor(byte _DoorId, byte _doorZoneStyleNum) {
			DoorZoneStyleNum = _doorZoneStyleNum;
			this.DoorId = _DoorId;
			itemImgPath = @"img\map\" + DoorZoneStyleNum.ToString() + @"\openDoor_" + DoorId.ToString();
		}

		public override bool CardUsed(Creature.Player pl) {
			if (pl.IsInBattle)
				return false;

			Tuple<byte, byte> doorPos = null;
			for (byte i = 0; i < pl.Map.SizeY; ++i) {
				for (byte j = 0; j < pl.Map.SizeX; ++j) {
					if(pl.Map[i, j].IsDoor && pl.Map[i, j].DoorId == DoorId && !pl.Map[i, j].IsInFog) {
						doorPos = new Tuple<byte, byte>(i, j);
						break;
					}
				}
			}

			if(doorPos != null) {
				if(pl.Map[doorPos.Item1, doorPos.Item2].DoorId == 0) {
					pl.Map.NewLevel(pl);
					return true;
				}
				else {
					pl.Map[doorPos.Item1, doorPos.Item2].IsDoorOpened = true;
					pl.PosX = doorPos.Item2;
					pl.PosY = doorPos.Item1;
					pl.PosChanged();
					return true;
				}
			}

			return false;
		}
	}
}
