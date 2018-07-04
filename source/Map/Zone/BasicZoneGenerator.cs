using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map.Zone {
	abstract class BasicZoneGenerator {
		protected List<Tuple<byte, byte, bool>> doorPos = new List<Tuple<byte, byte, bool>>();
		protected char lastZone;

		abstract public void GenerateMap(GameMap map);
		abstract public void PlaceMonster(GameMap map);
		abstract public void PlaceItems(GameMap map);
		abstract public void PlacePlayer(GameMap map, Creature.Player player);

		protected void PlaceDoors(GameMap map) {
			lastZone = 'a';

			foreach (var i in doorPos) {
				if(!map[(byte)(i.Item1 - 1), i.Item2].IsSolid)
					SetZoneLetters((byte)(i.Item1 - 1), i.Item2, lastZone, map);
				else if (!map[(byte)(i.Item1 + 1), i.Item2].IsSolid)
					SetZoneLetters((byte)(i.Item1 + 1), i.Item2, lastZone, map);
				else if (!map[i.Item1, (byte)(i.Item2 - 1)].IsSolid)
					SetZoneLetters((byte)(i.Item1), (byte)(i.Item2 - 1), lastZone, map);
				else if (!map[i.Item1, (byte)(i.Item2 + 1)].IsSolid)
					SetZoneLetters((byte)(i.Item1), (byte)(i.Item2 + 1), lastZone, map);

				map[i.Item1, i.Item2].IsWall = false;
				map[i.Item1, i.Item2].IsSolid = true;
				map[i.Item1, i.Item2].IsDoorOpened = false;
				map[i.Item1, i.Item2].IsDoor = true;
				map[i.Item1, i.Item2].IsDoorToNextLevel = i.Item3;
				map[i.Item1, i.Item2].CellZone = lastZone;
				PlaceDoorCardInZone(map, i);
				++lastZone;
			}

			doorPos.Clear();
		}

		protected virtual void PlaceDoorCardInZone(GameMap map, Tuple<byte, byte, bool> door) {
			var cell = map[(byte)(door.Item1 + 1), (byte)(door.Item2)];
			if (cell != null && !cell.IsSolid && cell.CellZone == lastZone) 
				cell.CellContent = new Card.Card(new Spell.Move.OpenDoor(lastZone));
			else {
				cell = map[(byte)(door.Item1 - 1), (byte)(door.Item2)];
				if (cell != null && !cell.IsSolid && cell.CellZone == lastZone)
					cell.CellContent = new Card.Card(new Spell.Move.OpenDoor(lastZone));
				else {
					cell = map[(byte)(door.Item1), (byte)(door.Item2 + 1)];
					if (cell != null && !cell.IsSolid && cell.CellZone == lastZone)
						cell.CellContent = new Card.Card(new Spell.Move.OpenDoor(lastZone));
					else {
						cell = map[(byte)(door.Item1), (byte)(door.Item2 - 1)];
						if (cell != null && !cell.IsSolid && cell.CellZone == lastZone)
							cell.CellContent = new Card.Card(new Spell.Move.OpenDoor(lastZone));
					}
				}
			}
		}

		void SetZoneLetters(byte i, byte j, char letter, GameMap map) {
			if (map[i, j].CellZone == 0 && !map[i, j].IsSolid) {
				map[i, j].CellZone = letter;
				SetZoneLetters(i, (byte)(j + 1), letter, map);
				SetZoneLetters(i, (byte)(j - 1), letter, map);
				SetZoneLetters((byte)(i + 1), j, letter, map);
				SetZoneLetters((byte)(i - 1), j, letter, map);
			}
		}
	}
}
