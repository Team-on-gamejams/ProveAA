using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map.Zone {
	class ZoneTest : BasicZoneGenerator {
		List<Tuple<byte, byte, byte>> doorPos = new List<Tuple<byte, byte, byte>>();

		public override void GenerateMap(GameMap map, Creature.Player player) {
			PlaceMap(map);
			PlaceItems(map);
			PlaceMonster(map);
			PlacePlayer(map, player);
		}

		public void PlaceMap(GameMap map) {
			doorPos.Clear();
			for (byte i = 1; i < map.SizeX - 1; ++i)
				map[2, i].IsSolid = map[2, i].IsWall = true;

			//map[1, (byte)(map.SizeX - 2)].CellContent = new Card.Card(new Spell.Move.OpenDoor(1));
			//doorPos.Add(new Tuple<byte, byte, byte>(2, (byte)(map.SizeX - 2), 1));

			//map[(byte)(map.SizeY - 2), 1].CellContent = new Card.Card(new Spell.Move.OpenDoor(0));
			//doorPos.Add(new Tuple<byte, byte, byte>((byte)(map.SizeY - 1), 1, 0));


			foreach (var i in doorPos) {
				map[i.Item1, i.Item2].IsWall = false;
				map[i.Item1, i.Item2].IsSolid = true;
				map[i.Item1, i.Item2].IsDoorOpened = false;
				map[i.Item1, i.Item2].IsDoor = true;
				map[i.Item1, i.Item2].DoorId = i.Item3;
			}
		}

		public void PlaceItems(GameMap map) {
			map[1, (byte)(map.SizeX - 3)].CellContent = new Card.Card(new Item.Potion.ManaPotion());
			map[(byte)(map.SizeY - 2), (byte)(map.SizeX - 2)].CellContent = new Card.Card(new Item.Armor.MetallShield());
			map[(byte)(map.SizeY - 3), (byte)(map.SizeX - 2)].CellContent = new Card.Card(new Item.Weapon.Spear());
			map[(byte)(map.SizeY - 3), (byte)(map.SizeX - 3)].CellContent = new Card.Card(new Spell.Attack.Fireball());
		}

		public void PlaceMonster(GameMap map) {
		//	map[(byte)(map.SizeY - 3), (byte)(map.SizeX - 4)].CellContent = new Creature.Monster.Ghost1(player);
		}

		public void PlacePlayer(GameMap map, Creature.Player player) {
			for (byte i = 1; i < map.SizeY - 1; ++i)
				for (byte j = 1; j < map.SizeX - 1; ++j) {
					if (!map[i, j].IsSolid) {
						player.PosX = j;
						player.PosY = i;
						player.PosChanged();
						return;
					}
				}
		}
	}
}
