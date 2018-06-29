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

namespace ProveAA.Map {
	class GameCell {
		bool isSolid, isWall, isDoor, isInFog;

		public bool IsSolid { get => isSolid; set { isSolid = value; RecreateImage(); } }
		public bool IsWall { get => isWall; set { isWall = value; RecreateImage(); } }
		public bool IsDoor { get => isDoor; set { isDoor = value; RecreateImage(); } }
		public bool IsInFog { get => isInFog; set { isInFog = value; RecreateImage(); } }

		Uri lastImage;
		public Image image;

		public GameCell() {
			image = new Image();
			isSolid = isWall = isInFog = true;
			isDoor = false;
			RecreateImage();
		}

		public void RecreateImage() {
			Uri newImage;
			if (IsInFog)
				newImage = new Uri(Environment.CurrentDirectory + @"\img\map\fog.png", UriKind.Absolute);
			else if (IsWall)
				newImage =new Uri(Environment.CurrentDirectory + @"\img\map\wall.png", UriKind.Absolute);
			else if (IsDoor && IsSolid)
				newImage =new Uri(Environment.CurrentDirectory + @"\img\map\doorClosed.png", UriKind.Absolute);
			else if (IsDoor && !IsSolid)
				newImage =new Uri(Environment.CurrentDirectory + @"\img\map\doorOpened.png", UriKind.Absolute);
			else if (!IsSolid)
				newImage =new Uri(Environment.CurrentDirectory + @"\img\map\road.png", UriKind.Absolute);
			else
				newImage =new Uri(Environment.CurrentDirectory + @"\img\map\other.png", UriKind.Absolute);
			if (newImage != lastImage) {
				lastImage = newImage;
				image.Source = new BitmapImage(lastImage);
			}
		}
	}
}
