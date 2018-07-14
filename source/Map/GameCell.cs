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
		private byte zoneStyleNum;
		bool isSolid, isWall,isWallFore, isDoor, isInFog, isDoorOpened;
		private Interface.ICellContent cellContent;
		public bool IsVisited { get => _isVisited; set{ _isVisited = value; RecreateImage(); } }
		public bool IsSolid { get => isSolid; set { isSolid = value; RecreateImage(); } }
		public bool IsWallFore { get => isWallFore; set { isWallFore = value; RecreateImage(); } }
		public bool IsWall { get => isWall; set { isWall = value; isSolid = value; RecreateImage(); } }
		public bool IsDoorOpened { get => isDoorOpened; set { isDoorOpened = value; IsSolid = !value; } }
		public bool IsDoor { get => isDoor; set {isWall = false; isDoor = value; IsDoorOpened = false; } }
		public byte DoorId { get; set; }

		public bool IsInFog { get => isInFog; set { if (isInFog == value) return; isInFog = value; RecreateImage(); } }

		internal ICellContent CellContent { get => cellContent; set { cellContent = value; if (!IsInFog) RecreateContentImage(); } }

		public byte ZoneStyleNum { get => zoneStyleNum; set { zoneStyleNum = value; RecreateImage(); } }

		Uri lastImage;
		public Image imageCell;
		public Image imageContent;
		private bool _isVisited;

		public GameCell() {
			imageCell = new Image();
			imageContent = new Image();
			RefillValue();
		}

		public void RefillValue() {
			IsVisited = false;
			isSolid = isWall = isInFog = true;
			isDoor = false;
			isDoorOpened = false;
			cellContent = null;
			zoneStyleNum = 0;
			RecreateImage();
			RecreateContentImage();
		}

		public void RecreateImage() {
			Uri newImage;
			if (IsInFog)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\" + ZoneStyleNum.ToString() + @"\fog.png", UriKind.Absolute);
			else if (IsWall && !isWallFore)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\" + ZoneStyleNum.ToString() + @"\wall.png", UriKind.Absolute);
			else if (IsWall && isWallFore)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\" + ZoneStyleNum.ToString() + @"\wallFore.png", UriKind.Absolute);
			else if (IsDoor && !isDoorOpened)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\" + ZoneStyleNum.ToString() + @"\doorClosed_" + DoorId.ToString() + ".png", UriKind.Absolute);
			else if (IsDoor && isDoorOpened)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\" + ZoneStyleNum.ToString() + @"\doorOpened_" + DoorId.ToString() + ".png", UriKind.Absolute);
			else if (IsVisited)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\" + ZoneStyleNum.ToString() + @"\visited.png", UriKind.Absolute);
			else if (!IsSolid)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\" + ZoneStyleNum.ToString() + @"\road.png", UriKind.Absolute);
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
			if (CellContent is Creature.Monster.BasicMonster) {
				Grid.SetRowSpan(imageContent, 2);
				if (Grid.GetRow(imageCell) != 0)
					Grid.SetRow(imageContent, Grid.GetRow(imageCell) - 1);
			}
			else {
				Grid.SetRowSpan(imageContent, 1);
				Grid.SetRow(imageContent, Grid.GetRow(imageCell));
			}
		}

	}
}
