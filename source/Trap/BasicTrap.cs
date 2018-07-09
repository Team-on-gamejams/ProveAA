using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Trap {
	abstract class BasicTrap : Interface.ICellContent {
		public Uri GetDisplayImage() => new Uri(Environment.CurrentDirectory + @"\img\Invisible.png", UriKind.Absolute); 

		public abstract bool PlayerStepIn(Player player);

		protected void PrintText(string text) {

		}
	}
}
