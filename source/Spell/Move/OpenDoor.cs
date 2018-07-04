using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Spell.Move {
	class OpenDoor : BasicSpell {
		char doorLetter;

		public OpenDoor(char DoorLetter) {
			this.DoorLetter = DoorLetter;
			itemImgPath += "OpenDoor" + this.DoorLetter.ToString().ToUpper();
		}

		public char DoorLetter { get => doorLetter; set => doorLetter = value; }

		public override bool CardUsed(Creature.Player pl) {
			if (pl.IsInBattle)
				return false;

			Tuple<byte, byte> doorPos = null;
			for (byte i = 0; i < pl.Map.SizeY; ++i) {
				for (byte j = 0; j < pl.Map.SizeX; ++j) {
					if(pl.Map[i, j].IsDoor && pl.Map[i, j].CellZone == DoorLetter && !pl.Map[i, j].IsInFog) {
						doorPos = new Tuple<byte, byte>(i, j);
						break;
					}
				}
			}

			if(doorPos != null) {
				if(pl.Map[doorPos.Item1, doorPos.Item2].IsDoorToNextLevel) {
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
