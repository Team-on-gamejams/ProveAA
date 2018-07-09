using System;
using System.Collections.Generic;
using ProveAA.Creature;

namespace ProveAA.Map.Zone {
	class MazeGenerator : BasicZoneGenerator {
		GameMap map;
		Stack<DiggerInfo> digInfo = new Stack<DiggerInfo>();

		public override void GenerateMap(GameMap _map, Player player) {
			map = _map;
			for (byte i = 0; i < map.SizeY; ++i)
				for (byte j = 0; j < map.SizeX; ++j)
					map[i, j].IsWall = true;

			byte startX = (byte)(map.SizeX / 2), startY = (byte)(map.SizeY/2);
			digInfo.Push(new DiggerInfo(startX, startY, 0));

			while(digInfo.Count != 0) 
				RecDig(digInfo.Pop());

			for (byte i = 0; i < map.SizeY; ++i) {
				for (byte j = 0; j < map.SizeX; ++j) {
					map[i, j].IsVisited = false;
					map[i, j].IsInFog = Game.Settings.mazeGen_PlaceFog;
				}
			}

			map[(byte)(startY), (byte)(startX - 3)].CellContent = new Card.Card(new Item.Weapon.Spear());
			map[(byte)(startY - 3), (byte)(startX)].CellContent = new Card.Card(new Item.Weapon.Spear());

			map[(byte)(startY - 1), (byte)(startX)].CellContent = new Card.Card(new Item.Armor.MetallShield());
			map[(byte)(startY - 2), (byte)(startX)].CellContent = new Card.Card(new Item.Weapon.GhostSlayer());
			map[(byte)(startY), (byte)(startX - 1)].CellContent = new Card.Card(new Spell.Attack.HolyWater());
			map[(byte)(startY), (byte)(startX - 2)].CellContent = new Card.Card(new Item.Potion.RefreshPotion());
			map[(byte)(startY), (byte)(startX + 1)].CellContent = new Creature.Monster.Graveyard.Ghost1(player);
			map[(byte)(startY), (byte)(startX + 2)].CellContent = new Creature.Monster.Graveyard.Ghost2(player);
			map[(byte)(startY + 1), (byte)(startX)].CellContent = new Creature.Monster.Graveyard.Ghost3(player);
			map[(byte)(startY + 2), (byte)(startX)].CellContent = new Trap.HealthTrap();

			player.PosX = startX;
			player.PosY = startY;
			player.PosChanged();
		}

		void RecDig(DiggerInfo info) {
			map[info.y, info.x].IsVisited = true;
			map[info.y, info.x].IsSolid = map[info.y, info.x].IsWall = false;


			List<DiggerInfo> readyPos = new List<DiggerInfo>();
			List<DiggerInfo> tmpPos = new List<DiggerInfo>() {
				new DiggerInfo((byte)(info.x + 2), (byte)(info.y), (byte)(info.itterCnt + 1)),
				new DiggerInfo((byte)(info.x - 2), (byte)(info.y), (byte)(info.itterCnt + 1)),
				new DiggerInfo((byte)(info.x), (byte)(info.y + 2), (byte)(info.itterCnt + 1)),
				new DiggerInfo((byte)(info.x), (byte)(info.y - 2), (byte)(info.itterCnt + 1)),
			};

			foreach (var i in tmpPos) {
				if (!(map[i.y, i.x] == null || i.x == 0 || i.y == 0 || i.x == map.SizeX - 1 || i.y == map.SizeY - 1))
					if(Game.Settings.mazeGen_crossOnItterCnt.Contains(info.itterCnt) || !map[i.y, i.x].IsVisited)
						readyPos.Add(i);
			}

			if (readyPos.Count != 0) {
				byte directions = (byte)Game.Rand.Next(1, readyPos.Count);
				if (Game.Settings.mazeGen_crossOnItterCnt.Contains(info.itterCnt))
					directions = (byte)readyPos.Count;
				while (directions-- != 0) {
					byte randInfo = (byte)Game.Rand.Next(0, readyPos.Count);
					digInfo.Push(readyPos[randInfo]);

					if(readyPos[randInfo].x == info.x - 2)
						map[(byte)(info.y), (byte)(info.x - 1)].IsWall = false;
					if (readyPos[randInfo].x == info.x + 2)
						map[(byte)(info.y), (byte)(info.x + 1)].IsWall = false;
					if (readyPos[randInfo].y == info.y + 2)
						map[(byte)(info.y + 1), (byte)(info.x)].IsWall = false;
					if (readyPos[randInfo].y == info.y - 2)
						map[(byte)(info.y - 1), (byte)(info.x)].IsWall = false;

					readyPos.RemoveAt(randInfo);
				}
			}
		}

		class DiggerInfo {
			public byte x, y;
			public ushort itterCnt;

			public DiggerInfo(byte x, byte y, ushort itterCnt) {
				this.x = x;
				this.y = y;
				this.itterCnt = itterCnt;
			}
		}
	}
}
