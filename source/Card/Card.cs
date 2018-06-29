using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveAA.Interface;
using ProveAA.Support;
using ProveAA.Game;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProveAA.Card {
	class Card {
		Grid cardGrid;
		ICardContent cardContent;

		public Card(ICardContent content) {
			cardContent = content;
		}

		public void AddToHand(Creature.Player player, Windows.GameWindow window) {
			if (player.Cards.Count < 7) {
				cardGrid = new Grid();
				cardGrid.MouseLeftButtonUp += (a, b) => this.Use(player);

				cardGrid.Children.Add(new TextBlock() { Text = "I am card, isnt it?" });

				Grid.SetColumn(cardGrid, player.Cards.Count - 1);
				window.CardsGrid.Children.Add(cardGrid);
			}
		}

		public void Use(Creature.Player player) {
			cardContent.CardUsed(player);
			if (player.Cards.ElementAt(0) != this) {
				player.Cards.Remove(this);
				for (byte i = 0; i < player.Cards.Count; ++i)
					Grid.SetColumn(player.Cards[i].cardGrid, i);
			}
		}
	}
}
