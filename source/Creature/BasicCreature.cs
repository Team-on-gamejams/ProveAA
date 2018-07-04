using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ProveAA.Interface;
using ProveAA.Support;
using ProveAA.Game;


namespace ProveAA.Creature {
	class BasicCreature {
		public readonly Stat armor;
		public readonly Stat attack;
		public readonly Bar hitPoints;
		public readonly Bar manaPoints;

		protected Grid imageGridMaze;

		public BasicCreature() {
			imageGridMaze = new Grid();
			Grid.SetZIndex(imageGridMaze, 10);
			armor = new Stat();
			attack = new Stat();
			hitPoints = new Bar();
			manaPoints = new Bar();
		}

		public virtual bool GetDmgWithArmor(byte dmgIn) {
			short dmg = (short)(dmgIn - this.armor.Current);
			if (dmg <= 0)
				dmg = 1;
			if (dmg > hitPoints.Current)
				dmg = hitPoints.Current;
			hitPoints.Current -= (byte)dmg;
			return hitPoints.Current == 0;
		}

		public virtual bool GetAttack(BasicCreature Enemy) {
			short dmg = (short)(Enemy.attack.Current - this.armor.Current);
			if (dmg <= 0)
				dmg = 1;
			if (dmg > hitPoints.Current)
				dmg = hitPoints.Current;
			hitPoints.Current -= (byte)dmg;
			return hitPoints.Current == 0;
		}

		static public Grid CloneGrid(Grid grid) {
			string gridXaml = System.Windows.Markup.XamlWriter.Save(grid);
			System.IO.StringReader stringReader = new System.IO.StringReader(gridXaml);
			System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(stringReader);
			return (Grid)System.Windows.Markup.XamlReader.Load(xmlReader);
		}
	}
}
