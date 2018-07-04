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

namespace ProveAA.Map {
	class GameCell {
		private char _cellZone;
		bool isSolid, isWall, isDoor, isInFog, isVisited, isDoorOpened;
		private Interface.ICellContent cellContent;
		public bool IsVisited { get => isVisited; set => isVisited = value; }
		public bool IsSolid { get => isSolid; set { isSolid = value; RecreateImage(); } }
		public bool IsWall { get => isWall; set { isWall = value; RecreateImage(); } }
		public bool IsDoorOpened { get => isDoorOpened; set { isDoorOpened = value; IsSolid = !value; RecreateImage(); } }
		public bool IsDoor { get => isDoor; set { isDoor = value; RecreateImage(); } }
		public bool IsInFog { get => isInFog; set { isInFog = value; RecreateImage(); } }
		public char CellZone { get => _cellZone;
			set {
				_cellZone = value;
				imageLetter.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\letter\" + _cellZone.ToString().ToLower() + ".png", UriKind.Absolute));
			}
		}

		internal ICellContent CellContent { get => cellContent; set { cellContent = value; if (!IsInFog) RecreateContentImage(); } }

		Uri lastImage;
		public Image imageCell;
		public Image imageLetter;
		public Image imageContent;

		public GameCell() {
			imageLetter = new Image();
			imageLetter.Height = imageLetter.Width = 20;
			imageCell = new Image();
			imageContent = new Image();
			RefillValue();
		}

		public void RefillValue() {
			isVisited = false;
			isSolid = isWall = isInFog = true;
			isDoor = false;
			isDoorOpened = false;
			_cellZone = new char();
			cellContent = null;
			RecreateImage();
		}

		public void RecreateImage() {
			Uri newImage;
			if (IsInFog)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\fog.png", UriKind.Absolute);
			else if (IsWall)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\wall.png", UriKind.Absolute);
			else if (IsDoor && !isDoorOpened)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\doorClosed.png", UriKind.Absolute);
			else if (IsDoor && isDoorOpened)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\doorOpened.png", UriKind.Absolute);
			else if (IsVisited)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\visited.png", UriKind.Absolute);
			else if (!IsSolid)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\road.png", UriKind.Absolute);
			else
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\other.png", UriKind.Absolute);

			if (!IsInFog)
				RecreateContentImage();

			if (newImage != lastImage) {
				lastImage = newImage;
				imageCell.Source = new BitmapImage(lastImage);
			}
		}

		public void RecreateContentImage() {
			if (CellContent != null)
				imageContent.Source = new BitmapImage(CellContent.GetDisplayImage());
			else
				imageContent.Source = null;
		}

	}
}
