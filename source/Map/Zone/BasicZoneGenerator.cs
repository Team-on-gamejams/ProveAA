using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map.Zone {
	abstract class BasicZoneGenerator {
		protected char lastZone = 'a';

		abstract public void GenerateMap(GameMap map);
		abstract public void PlaceMonster(GameMap map);
		abstract public void PlaceItems(GameMap map);
		abstract public void PlacePlayer(GameMap map, Creature.Player player);

		protected void SetZoneLetters(byte i, byte j, char letter, GameMap map) {
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
