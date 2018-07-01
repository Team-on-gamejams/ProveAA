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
		}

		public override bool CardUsed(Creature.Player pl) {
			Tuple<byte, byte> doorPos = null;
			for (byte i = 1; i < pl.Map.SizeY - 1; ++i) {
				for (byte j = 1; j < pl.Map.SizeX - 1; ++j) {
					if(pl.Map[i, j].IsDoor && pl.Map[i, j].CellZone == doorLetter && !pl.Map[i, j].IsInFog) {
						doorPos = new Tuple<byte, byte>(i, j);
						break;
					}
				}
			}

			if(doorPos != null) {
				pl.Map[doorPos.Item1, doorPos.Item2].IsDoorOpened = true;
				byte i = (byte)(doorPos.Item1 + 1), j= doorPos.Item2;

				if (!TrySetPos()) {
					i -= 2;
					if (!TrySetPos()) {
						++i;
						++j;
						if (!TrySetPos()) {
							j -= 2;
							TrySetPos();
						}
					}
				}
				return true;

				bool TrySetPos() {
					if (pl.Map[i, j].CellZone == doorLetter) {
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

		public override Uri GetImageForCard() {
			return new Uri(Environment.CurrentDirectory + @"\img\spell\OpenDoor" + doorLetter.ToString().ToLower() + "Card.png", UriKind.Absolute);
		}

		public override Uri GetImageForCell() {
			return new Uri(Environment.CurrentDirectory + @"\img\spell\OpenDoor" + doorLetter.ToString().ToLower() + "Cell.png", UriKind.Absolute);
		}
	}
}
