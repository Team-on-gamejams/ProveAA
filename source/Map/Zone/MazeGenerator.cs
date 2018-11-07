using System;
using System.Collections.Generic;
using ProveAA.Creature;

namespace ProveAA.Map.Zone {
	class MazeGenerator : BasicZoneGenerator {
		GameMap map;
		Stack<DiggerInfo> digInfo = new Stack<DiggerInfo>();

		List<DoorInfo> doorExitPos = new List<DoorInfo>();
		List<DoorInfo> doorInfoAll = new List<DoorInfo>();
		List<DoorInfo> doorInfoReal = new List<DoorInfo>();
		List<DoorInfo> doorForTreasure = new List<DoorInfo>();

		public MazeGenerator() {
			generatorZoneStyleNum = 0;
		}

		public override void GenerateMap(GameMap _map, Player player) {
			digInfo.Clear();
			doorExitPos.Clear();
			doorInfoAll.Clear();
			doorInfoReal.Clear();
			doorForTreasure.Clear();
			map = _map;

			for (byte i = 0; i < map.SizeY; ++i) {
				for (byte j = 0; j < map.SizeX; ++j) {
					map[i, j].ZoneStyleNum = this.generatorZoneStyleNum;
					map[i, j].IsVisited = false;
					map[i, j].IsInFog = Game.Settings.mazeGen_PlaceFog;
					map[i, j].IsWall = true;
				}
			}

			byte startX = (byte)(map.SizeX / 2), startY = (byte)(map.SizeY / 2);
			digInfo.Push(new DiggerInfo(startX, startY, 0));

			while (digInfo.Count != 0)
				RecDig(digInfo.Pop());
			// END init

			//Place player
			for (byte i = 0; i < map.SizeY; ++i) 
				for (byte j = 0; j < map.SizeX; ++j) 
					map[i, j].IsVisited = false;
			var newPlPos = NearestEmpty((byte)(map.SizeX - 1 - player.PosX), (byte)(map.SizeY - 1 - player.PosY));
			player.SetPosTo0();
			player.PosX = newPlPos.Item1;
			player.PosY = newPlPos.Item2;
			player.PosChanged();
			//END Place player

			//Place exit door
			for (byte i = 0; i < map.SizeY; ++i)
				for (byte j = 0; j < map.SizeX; ++j)
					map[i, j].IsVisited = false;
			FillListWithDoorExitPos();
			var exit = doorExitPos[Game.Rand.Next(0, doorExitPos.Count)];
			map[exit.y, exit.x].IsDoor = true;
			map[exit.y, exit.x].DoorId = 0;

			if (!map[(byte)(exit.y + 1), (byte)(exit.x)]?.IsWall ?? false)
				map[(byte)(exit.y + 1), (byte)(exit.x)].CellContent = new Card.Card(new Spell.Move.OpenDoor(0, generatorZoneStyleNum));
			if (!map[(byte)(exit.y - 1), (byte)(exit.x)]?.IsWall ?? false)
				map[(byte)(exit.y - 1), (byte)(exit.x)].CellContent = new Card.Card(new Spell.Move.OpenDoor(0, generatorZoneStyleNum));
			if (!map[(byte)(exit.y), (byte)(exit.x + 1)]?.IsWall ?? false)
				map[(byte)(exit.y), (byte)(exit.x + 1)].CellContent = new Card.Card(new Spell.Move.OpenDoor(0, generatorZoneStyleNum));
			if (!map[(byte)(exit.y), (byte)(exit.x - 1)]?.IsWall ?? false)
				map[(byte)(exit.y), (byte)(exit.x - 1)].CellContent = new Card.Card(new Spell.Move.OpenDoor(0, generatorZoneStyleNum));
			//END Place exit door


			for (byte i = 0; i < map.SizeY; ++i)
				for (byte j = 0; j < map.SizeX; ++j)
					map[i, j].IsVisited = false;
			FillListWithDoorPos();
			foreach (var door in doorInfoReal) {
				map[door.y, door.x].IsInFog = true;
			}


			map[(byte)(startY), (byte)(startX - 3)].CellContent = new Card.Card(new Item.Weapon.Spear());
			map[(byte)(startY - 3), (byte)(startX)].CellContent = new Card.Card(new Item.Weapon.Spear());

			map[(byte)(startY - 1), (byte)(startX)].CellContent = new Card.Card(new Item.Armor.MetallShield());
			map[(byte)(startY - 2), (byte)(startX)].CellContent = new Card.Card(new Item.Weapon.GhostSlayer());
			map[(byte)(startY), (byte)(startX - 1)].CellContent = new Card.Card(new Spell.Attack.HolyWater());
			map[(byte)(startY), (byte)(startX - 2)].CellContent = new Card.Card(new Item.Potion.HealingPotion3());
			map[(byte)(startY), (byte)(startX + 1)].CellContent = new Creature.Monster.Graveyard.Ghost1(player);
			map[(byte)(startY), (byte)(startX + 2)].CellContent = new Creature.Monster.Graveyard.Ghost2(player);
			map[(byte)(startY + 1), (byte)(startX)].CellContent = new Creature.Monster.Graveyard.Ghost3(player);
			map[(byte)(startY + 2), (byte)(startX)].CellContent = new Trap.HealthTrap();


			for (byte i = 0; i < map.SizeY - 1; ++i)
				for (byte j = 1; j < map.SizeX - 1; ++j)
					if (map[i, j].IsWall && (!map[(byte)(i + 1), j].IsSolid))
						map[i, j].IsWallFore = true;

			for (byte i = 0; i < map.SizeY; ++i) {
				for (byte j = 0; j < map.SizeX; ++j) {
					map[i, j].ZoneStyleNum = this.generatorZoneStyleNum;
					map[i, j].IsVisited = false;
					map[i, j].IsInFog = Game.Settings.mazeGen_PlaceFog;
				}
			}
		}

		void FillListWithDoorPos() {
			for (byte i = 0; i < map.SizeY; ++i) {
				for (byte j = 0; j < map.SizeX; ++j) {
					int cntWalls = ((map[(byte)(i + 1), j]?.IsWall ?? false)? 1 : 0) +
							((map[(byte)(i - 1), j]?.IsWall ?? false) ? 1 : 0) +
							((map[i, (byte)(j + 1)]?.IsWall ?? false) ? 1 : 0) +
							((map[i, (byte)(j - 1)]?.IsWall ?? false) ? 1 : 0);


					int cntRoads = ((map[(byte)(i + 1), j]?.IsSolid ?? true) ? 0 : 1) +
							((map[(byte)(i - 1), j]?.IsSolid ?? true) ? 0 : 1) +
							((map[i, (byte)(j + 1)]?.IsSolid ?? true) ? 0 : 1) +
							((map[i, (byte)(j - 1)]?.IsSolid ?? true) ? 0 : 1);

					bool isOpositeSame = (map[(byte)(i + 1), j]?.IsSolid ?? true) == (map[(byte)(i - 1), j]?.IsSolid ?? true) &&
						(map[i, (byte)(j + 1)]?.IsSolid ?? true) == (map[i, (byte)(j - 1)]?.IsSolid ?? true);

					if (map[i, j].IsSolid) {

					}
					else {
						if (cntWalls == 2) {
							if (isOpositeSame)
								doorInfoAll.Add(new DoorInfo(j, i));
						}
					}
				}
			}

			foreach (var door in doorInfoAll)
				if (TryPlaceDoor(door))
					doorInfoReal.Add(door);
			doorInfoReal.Sort(new Comparison<DoorInfo>((a, b) => { return a.ModSize - b.ModSize; }));
		}

		void FillListWithDoorExitPos() {
			for (byte i = 0; i < map.SizeY; ++i) {
				for (byte j = 0; j < map.SizeX; ++j) {
					int cntRoads = ((map[(byte)(i + 1), j]?.IsSolid ?? true) ? 0 : 1) +
							((map[(byte)(i - 1), j]?.IsSolid ?? true) ? 0 : 1) +
							((map[i, (byte)(j + 1)]?.IsSolid ?? true) ? 0 : 1) +
							((map[i, (byte)(j - 1)]?.IsSolid ?? true) ? 0 : 1);
					if (map[i, j].IsSolid && cntRoads == 1) 
						doorExitPos.Add(new DoorInfo(j, i));
					
				}
			}
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

		bool TryPlaceDoor(DoorInfo info) {
			DoorCellInfo[,] cells = new DoorCellInfo[map.SizeY, map.SizeX];
			for (byte i = 0; i < cells.GetLength(0); ++i)
				for (byte j = 0; j < cells.GetLength(1); ++j) 
					cells[i, j] = new DoorCellInfo(map[i, j]);
			cells[(byte)(info.y), info.x].isSolid = true;

			bool findLoop = false;
			if (map[info.y, (byte)(info.x + 1)].IsSolid) {
				Rec(info.x, (byte)(info.y + 1), 1);
				Rec(info.x, (byte)(info.y - 1), 2);
			}
			else {
				Rec((byte)(info.x + 1), info.y, 1);
				Rec((byte)(info.x - 1), info.y, 2);
			}

			if (!findLoop) {
				ushort size1 = 0, size2 = 0;
				for (byte i = 0; i < cells.GetLength(0); ++i)
					for (byte j = 0; j < cells.GetLength(1); ++j)
						if (cells[i, j].doorId == 1)
							++size1;
						else if (cells[i, j].doorId == 2)
							++size2;
				info.sizePart1 = size1;
				info.sizePart2 = size2;
			}

			return !findLoop;

			void Rec(byte x, byte y, byte doorId) {
				if (cells[y, x].doorId != 0 && cells[y, x].doorId != doorId)
					findLoop = true;
				if (findLoop || cells[y, x].isSolid || cells[y, x].doorId != 0)
					return;

				cells[y, x].doorId = doorId;
				Rec((byte)(x + 1), (byte)(y), doorId);
				Rec((byte)(x - 1), (byte)(y), doorId);
				Rec((byte)(x), (byte)(y + 1), doorId);
				Rec((byte)(x), (byte)(y - 1), doorId);
			}
		}

		Tuple<byte, byte> NearestEmpty(byte startX, byte startY) {
			byte x = startX, y = startY;
			if (x < 1)
				x = 1;
			if (y < 1)
				y = 1;

			Stack<Tuple<byte, byte>> recInfo = new Stack<Tuple<byte, byte>>();
			recInfo.Push(new Tuple<byte, byte>(x, y));
			while(recInfo.Count != 0)
				Rec(recInfo.Pop());

			return new Tuple<byte, byte>(x, y);

			void Rec(Tuple<byte, byte> pos) {
				if (!(map[pos.Item2, pos.Item1]?.IsVisited ?? true)) {
					recInfo.Push(new Tuple<byte, byte>((byte)(pos.Item1 + 1), pos.Item2));
					recInfo.Push(new Tuple<byte, byte>((byte)(pos.Item1 - 1), pos.Item2));
					recInfo.Push(new Tuple<byte, byte>(pos.Item1, (byte)(pos.Item2 + 1)));
					recInfo.Push(new Tuple<byte, byte>(pos.Item1, (byte)(pos.Item2 - 1)));

					map[pos.Item2, pos.Item1].IsVisited = true;
					if (!(map[pos.Item2, pos.Item1].IsWall || map[pos.Item2, pos.Item1].IsSolid || map[pos.Item2, pos.Item1].IsDoor)) {
						x = pos.Item1;
						y = pos.Item2;
						recInfo.Clear();
					}

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

		class DoorInfo {
			public byte x, y;
			public ushort sizePart1, sizePart2;
			public ushort ModSize => (ushort)Math.Abs(sizePart1 - sizePart2);
			public ushort Min => Math.Min(sizePart1,  sizePart2);
			public ushort Max => Math.Max(sizePart1, sizePart2);
			public DoorInfo(byte x, byte y) {
				this.x = x;
				this.y = y;
			}
		}

		class DoorCellInfo {
			public bool isSolid;
			public byte doorId;

			public DoorCellInfo(Map.GameCell cell) {
				isSolid = cell.IsSolid;
			}
		}

	}
}
