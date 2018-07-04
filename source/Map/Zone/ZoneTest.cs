﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map.Zone {
	class ZoneTest : BasicZoneGenerator {
		public override void GenerateMap(GameMap map) {

			for (byte i = 1; i < map.SizeX - 1; ++i)
				map[2, i].IsSolid = map[2, i].IsWall = true;
			doorPos.Add(new Tuple<byte, byte, bool>(2, (byte)(map.SizeX - 2), false));
			doorPos.Add(new Tuple<byte, byte, bool>((byte)(map.SizeY - 1), 1, true));

			PlaceDoors(map);
		}

		public override void PlaceItems(GameMap map) {
			map[1, (byte)(map.SizeX - 3)].CellContent = new Card.Card(new Item.Potion.ManaPotion());
			map[(byte)(map.SizeY - 2), (byte)(map.SizeX - 2)].CellContent = new Card.Card(new Item.Armor.MetallShield());
			map[(byte)(map.SizeY - 3), (byte)(map.SizeX - 2)].CellContent = new Card.Card(new Item.Weapon.Spear());
			map[(byte)(map.SizeY - 3), (byte)(map.SizeX - 3)].CellContent = new Card.Card(new Spell.Attack.Fireball());
		}

		public override void PlaceMonster(GameMap map) {
			map[(byte)(map.SizeY - 3), (byte)(map.SizeX - 4)].CellContent = new Creature.Monster.Ghost();
		}

		public override void PlacePlayer(GameMap map, Creature.Player player) {
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