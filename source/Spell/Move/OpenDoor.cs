using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Spell.Move {
	class OpenDoor : BasicSpell {
		char doorLetter;

		public OpenDoor(char DoorLetter) {
			doorLetter = DoorLetter;
			itemImgPath += "OpenDoor" + doorLetter.ToString().ToUpper();
		}

		public override bool CardUsed(Creature.Player pl) {
			if (pl.IsInBattle)
				return false;

			Tuple<byte, byte> doorPos = null;
			for (byte i = 0; i < pl.Map.SizeY; ++i) {
				for (byte j = 0; j < pl.Map.SizeX; ++j) {
					if(pl.Map[i, j].IsDoor && pl.Map[i, j].CellZone == doorLetter && !pl.Map[i, j].IsInFog) {
						doorPos = new Tuple<byte, byte>(i, j);
						break;
					}
				}
			}

			if(doorPos != null) {
				byte i = (byte)(doorPos.Item1 + 1), j= doorPos.Item2;
				if (!TrySetPos()) {
					i -= 2;
					if (!TrySetPos()) {
						++i;
						++j;
						if (!TrySetPos()) {
							j -= 2;
							if (!TrySetPos()) {
								pl.Map.NewLevel(pl);
								return true;
							}
						}
					}
				}
				return true;

				bool TrySetPos() {
					if (i < pl.Map.SizeY && j < pl.Map.SizeX && pl.Map[i, j].CellZone == doorLetter+1) {
						pl.Map[doorPos.Item1, doorPos.Item2].IsDoorOpened = true;
						pl.PosX = j;
						pl.PosY = i;
						pl.PosChanged();
						return true;
					}
					return false;
				}
			}

			return false;
		}
	}
}
