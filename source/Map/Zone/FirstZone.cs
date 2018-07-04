using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Map.Zone {
	class FirstZone : ZoneTest {
		public override void GenerateMap(GameMap map) {

			PlaceDoors(map);
			byte roomCnt = 3;
			ushort times;
			byte x = 0, y = 0;
			bool isVertical;
			while (--roomCnt != 0) {
				times = 0;
				do {
					if (++times > 1000)
						break;
					x = (byte)Game.Rand.Next(0, map.SizeX);
					y = (byte)Game.Rand.Next(0, map.SizeY);
				} while (map[y, x].IsSolid || 
				map[(byte)(y + 1), x].IsSolid || map[(byte)(y - 1), x].IsSolid || map[y, (byte)(x + 1)].IsSolid || map[y, (byte)(x - 1)].IsSolid || 
				map[(byte)(y + 2), x].IsSolid || map[(byte)(y - 2), x].IsSolid || map[y, (byte)(x + 2)].IsSolid || map[y, (byte)(x - 2)].IsSolid /*||
				map[(byte)(y + 3), x].IsSolid || map[(byte)(y - 3), x].IsSolid || map[y, (byte)(x + 3)].IsSolid || map[y, (byte)(x - 3)].IsSolid*/);

				if (++times > 1000)
					continue;

				isVertical = Game.Rand.Next(0, 2) == 1;
				//PlaceToEnd(x, y);
				/*
					void PlaceToEnd(ushort X, ushort Y) {
					if(wall[Y][X] == '-' || wall[Y][X] == '|') {
						wall[Y][X] = '#';
						return;
					}
					if (wall[Y][X] == '#') {
						if (Y == sizey - 5) {
							if (Rand.rand.Next(0, 2) == 1)
								wall[Y][X - 1] = '-';
							else
								wall[Y][X + 1] = '-';
						}
						else 
				{
					int xx = X, yy = Y;
					if (vertical) {
						if (xx - 1 < 0)
							++xx;
						else
							--xx;
					}
					else {
						if (yy - 1 < 0)
							++yy;
						else
							--yy;
					}
					if (wall[yy][xx] == '#')
						wall[yy][xx] = vertical ? '-' : '|';
				}
				return;
			}

			wall[Y][X] = '#';
			if (vertical) {
				PlaceToEnd(X, (ushort)(Y + 1));
				PlaceToEnd(X, (ushort)(Y - 1));
			}
			else {
				PlaceToEnd((ushort)(X + 1), Y);
				PlaceToEnd((ushort)(X - 1), Y);
			}
		}
			
	*/

				doorPos.Add(new Tuple<byte, byte, bool>(y, x, false));
			}

			doorPos.Add(new Tuple<byte, byte, bool>((byte)(map.SizeY - 2), (byte)(map.SizeX - 1), true));

			PlaceDoors(map);
		}

		protected override void PlaceDoorCardInZone(GameMap map, Tuple<byte, byte, bool> door) {
			List<Tuple<byte, byte>> zoneCells = new List<Tuple<byte, byte>>();
			for (byte i = 0; i < map.SizeY; ++i)
				for (byte j = 0; j < map.SizeX; ++j)
					if (map[i, j].CellZone == lastZone)
						zoneCells.Add(new Tuple<byte, byte>(i, j));
			Tuple<byte, byte> cellCoord;
			GameCell cell;
			do {
				cellCoord = zoneCells[Game.Rand.Next(0, zoneCells.Count)];
				cell = map[cellCoord.Item1, cellCoord.Item2];
			} while (cell.CellContent != null || cell.IsSolid);

			cell.CellContent = new Card.Card(new Spell.Move.OpenDoor(lastZone));
		}

		public override void PlaceItems(GameMap map) {
			//throw new NotImplementedException();
		}

		public override void PlaceMonster(GameMap map) {
			//throw new NotImplementedException();
		}
	}
}
