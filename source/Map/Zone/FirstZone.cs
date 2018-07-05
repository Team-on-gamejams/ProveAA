using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Map.Zone {
	class FirstZone : ZoneTest {
		//public override void GenerateMap(GameMap map) {
		//	lastZone = 'a';
		//	byte wallCnt = 2;
		//	ushort times;
		//	byte x = 0, y = 0;
		//	bool isVertical;
		//	while (wallCnt-- != 0) {
		//		isVertical = Game.Rand.Next(0, 2) == 1;
		//		times = 0;
		//		do {
		//			if (++times > 1000)
		//				break;
		//			x = (byte)Game.Rand.Next(0, map.SizeX);
		//			y = (byte)Game.Rand.Next(0, map.SizeY);
		//		} while (map[y, x].IsSolid || 
		//		map[(byte)(y + 1), x].IsSolid || map[(byte)(y - 1), x].IsSolid || map[y, (byte)(x + 1)].IsSolid || map[y, (byte)(x - 1)].IsSolid /*||
		//		map[(byte)(y + 2), x].IsSolid || map[(byte)(y - 2), x].IsSolid || map[y, (byte)(x + 2)].IsSolid || map[y, (byte)(x - 2)].IsSolid *//*||
		//		map[(byte)(y + 3), x].IsSolid || map[(byte)(y - 3), x].IsSolid || map[y, (byte)(x + 3)].IsSolid || map[y, (byte)(x - 3)].IsSolid*/);

		//		if (++times > 1000)
		//			continue;

		//		PlaceWallToEnd(x, y, (byte)(isVertical?1:0));
		//		PlaceWallToEnd(x, y, (byte)(isVertical?3:2));
		//		doorPos.Add(new Tuple<byte, byte, bool>(y, x, false));
		//	}

		//	doorPos.Add(new Tuple<byte, byte, bool>((byte)(map.SizeY - 2), (byte)(map.SizeX - 1), true));

		//	PlaceDoors(map);

		//	void PlaceWallToEnd(byte currX, byte currY, byte direction) {
		//		map[currY, currX].IsWall = map[currY, currX].IsSolid = true;
		//		switch (direction) {
		//			case 0:
		//				--currX;
		//			break;
		//			case 1:
		//				--currY;
		//			break;
		//			case 2:
		//				++currX;
		//			break;
		//			case 3:
		//				++currY;
		//			break;
		//		}
		//		if(!map[currY, currX].IsSolid)
		//			PlaceWallToEnd(currX, currY, direction);
		//	}
		//}

		//protected void PlaceDoorCardInZone(GameMap map, Tuple<byte, byte, bool> door) {
		//	List<Tuple<byte, byte>> zoneCells = new List<Tuple<byte, byte>>();
		//	for (byte i = 0; i < map.SizeY; ++i)
		//		for (byte j = 0; j < map.SizeX; ++j)
		//			if (map[i, j].CellZone == lastZone)
		//				zoneCells.Add(new Tuple<byte, byte>(i, j));
		//	Tuple<byte, byte> cellCoord;
		//	GameCell cell;
		//	do {
		//		cellCoord = zoneCells[Game.Rand.Next(0, zoneCells.Count)];
		//		cell = map[cellCoord.Item1, cellCoord.Item2];
		//	} while (cell.CellContent != null || cell.IsSolid);

		//	cell.CellContent = new Card.Card(new Spell.Move.OpenDoor(lastZone));
		//}

		//public void PlaceItems(GameMap map) {
		//	List<Item.BasicItem> items = new List<Item.BasicItem>() {
		//		new Item.Armor.MetallShield(),
		//		new Item.Weapon.Spear(),
		//		new Item.Potion.HealingPotion(),
		//		new Item.Potion.HealingPotion(),
		//		new Item.Potion.ManaPotion(),
		//		new Item.Potion.ManaPotion(),
		//		new Item.Potion.RefreshPotion(),
		//	};
		//	ushort times = 0;
		//	byte x = 0, y = 0;
		//	foreach (var item in items) {
		//		times = 0;
		//		do {
		//			if (++times > 1000)
		//				break;
		//			x = (byte)Game.Rand.Next(0, map.SizeX);
		//			y = (byte)Game.Rand.Next(0, map.SizeY);
		//		} while (map[y, x].IsSolid || map[y, x].CellContent != null);
		//		if (++times > 1000)
		//			continue;
		//		map[y, x].CellContent = new Card.Card(item);
		//	}
		//}

		//public void PlaceMonster(GameMap map) {
		//	List<Creature.Monster.BasicMonster> monsters = new List<Creature.Monster.BasicMonster>() {
		//		new Creature.Monster.Ghost(),
		//		new Creature.Monster.Ghost(),
		//		new Creature.Monster.Ghost(),
		//		new Creature.Monster.Ghost(),
		//		new Creature.Monster.Ghost(),
		//		new Creature.Monster.Ghost(),
		//	};
		//	ushort times = 0;
		//	byte x = 0, y = 0;
		//	foreach (var monster  in monsters) {
		//		times = 0;
		//		do {
		//			if (++times > 1000)
		//				break;
		//			x = (byte)Game.Rand.Next(0, map.SizeX);
		//			y = (byte)Game.Rand.Next(0, map.SizeY);
		//		} while (map[y, x].IsSolid || map[y, x].CellContent != null);
		//		if (++times > 1000)
		//			continue;
		//		map[y, x].CellContent = monster;
		//	}
		//}
	}
}
