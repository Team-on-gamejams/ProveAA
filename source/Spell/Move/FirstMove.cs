using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Spell.Move {
	class FirstMove : BasicSpell {
		public static List<Tuple<byte, byte>> avaliableMove = new List<Tuple<byte, byte>>();

		public override void UseCard(Player pl) {
			if(avaliableMove.Count == 0)
				RecFill(pl.PosX, pl.PosY);

			ushort randIndex = (ushort)Game.Singletones.rand.Next(0, avaliableMove.Count);
			pl.PosX = avaliableMove[randIndex].Item1;
			pl.PosY = avaliableMove[randIndex].Item2;
			avaliableMove.RemoveAt(randIndex);

			void RecFill(byte x, byte y) {
				if(!Game.Singletones.game.Map[x, y].IsSolid && !Game.Singletones.game.Map[x, y].IsInFog) {
					avaliableMove.Add(new Tuple<byte, byte>(x, y));
					RecFill(x, (byte)(y + 1));
					RecFill(x, (byte)(y - 1));
					RecFill((byte)(x + 1), y);
					RecFill((byte)(x - 1), y);
				}
			}
		}
	}
}
