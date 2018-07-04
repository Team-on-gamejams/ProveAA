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
using ProveAA.Creature;

namespace ProveAA.Card {
	class Card : Interface.ICellContent {
		Grid cardGrid;
		ICardContent cardContent;
		public static Windows.GameWindow window;

		internal ICardContent CardContent { get => cardContent; }

		public Card(ICardContent content) {
			cardContent = content;
		}

		public bool AddToHand(Creature.Player player) {
			if (player.Cards.Count < 7) {
				player.Cards.Add(this);
				cardGrid = new Grid();
				cardGrid.Margin = new Thickness(10);

				cardGrid.MouseLeftButtonUp += (a, b) => {
					this.Use(player);
				};


				var img = new Image() { Source = new BitmapImage(cardContent.GetImageForCard()) };
				cardGrid.Children.Add(img);

				Grid.SetColumn(cardGrid, player.Cards.Count - 1);
				window.CardsGrid.Children.Add(cardGrid);
				return true;
			}
			return false;
		}

		public bool PlayerStepIn(Player player) {
			return AddToHand(player);
		}

		public void Use(Creature.Player player) {
			if (cardContent.CardUsed(player)) {
				player.Cards.Remove(this);
				window.CardsGrid.Children.Remove(cardGrid);
				for (byte i = 0; i < player.Cards.Count; ++i)
					Grid.SetColumn(player.Cards[i].cardGrid, i);
				player.PlayerStepInCell(player.Map[player.PosY, player.PosX]);
			}
		}

		public Uri GetDisplayImage() {
			return cardContent.GetImageForCell();
		}
	}
}
