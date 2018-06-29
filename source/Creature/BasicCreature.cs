using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveAA.Interface;
using ProveAA.Support;


namespace ProveAA.Creature {
	class BasicCreature : ICellContent {
		public readonly Level level;
		public readonly Stat armor;
		public readonly Stat attack;
		public readonly Bar hitPoints;
		public readonly Bar manaPoints;

		byte posX, posY;
		public byte PosX { get => posX; set => posX = value; }
		public byte PosY { get => posY; set => posY = value; }

		public BasicCreature() {
			level = new Level();
			armor = new Stat();
			attack = new Stat();
			hitPoints = new Bar();
			manaPoints = new Bar();
		}
	}
}
