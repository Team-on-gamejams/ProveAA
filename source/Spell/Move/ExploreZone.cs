using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Spell.Move {
	class ExploreZone : BasicSpell {
		public static List<Tuple<byte, byte>> avaliableMove = new List<Tuple<byte, byte>>();

		public override bool CardUsed(Player pl) {
			if (pl.IsInBattle)
				return false;

			List<Tuple<byte, byte>> visitedPos = new List<Tuple<byte, byte>>();
			avaliableMove.Clear();
			if (avaliableMove.Count == 0)
				RecFill(pl.PosX, pl.PosY);

			if (avaliableMove.Count != 0) {
				ushort randIndex = (ushort)Game.Rand.Next(0, avaliableMove.Count);
				pl.PosX = avaliableMove[randIndex].Item1;
				pl.PosY = avaliableMove[randIndex].Item2;
				pl.PosChanged();
				avaliableMove.RemoveAt(randIndex);
				return true;
			}
			return false;

			void RecFill(byte x, byte y) {
				if (visitedPos.Contains(new Tuple<byte, byte>(x, y)))
					return;

				visitedPos.Add(new Tuple<byte, byte>(x, y));
				if (!pl.Map[y, x].IsSolid && !pl.Map[y, x].IsInFog) {
					if(!pl.Map[y, x].IsVisited)
						avaliableMove.Add(new Tuple<byte, byte>(x, y));
					RecFill(x, (byte)(y + 1));
					RecFill(x, (byte)(y - 1));
					RecFill((byte)(x + 1), y);
					RecFill((byte)(x - 1), y);
				}
			}
		}

		public override Uri GetImageForCard() {
			return new Uri(Environment.CurrentDirectory + @"\img\spell\ExploreCard.png", UriKind.Absolute);
		}

		public override Uri GetImageForCell() {
			return GetImageForCard();
		}
	}
}
