﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Creature;

namespace ProveAA.Spell {
	abstract class BasicSpell : Interface.ICardContent {
		public abstract void UseCard(Player creature);
	}
}
