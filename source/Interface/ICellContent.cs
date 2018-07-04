using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Interface {
	interface ICellContent {
		bool PlayerStepIn(Creature.Player player);

		Uri GetDisplayImage();
	}
}
