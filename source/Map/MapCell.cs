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
	class MapCell {
		private bool isInFog;
		private bool isWall;
		private bool isDoor;
		private bool isSolid;
		private ICellContent cellContent;
		public System.Windows.Controls.Image image;

		public bool IsInFog { get => isInFog; set{ isInFog = value; ChangeImage(); } }
		internal ICellContent CellContent { get => cellContent; set{ cellContent = value; ChangeImage(); } }
		public bool IsWall { get => isWall; set{ isWall = value; isSolid = true; ChangeImage(); } }
		public bool IsDoor { get => isDoor; set { isDoor = value; isSolid = true; ChangeImage(); } }
		public bool IsSolid { get => isSolid; set{ isSolid = value; ChangeImage(); } }

		public MapCell() {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				image = new System.Windows.Controls.Image();
				Grid.SetZIndex(image, 0);
			});
		}

		void ChangeImage() {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				if (IsInFog)
					image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\map\fog.png", UriKind.Absolute));
				else if (IsWall)
					image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\map\wall.png", UriKind.Absolute));
				else if (IsDoor && IsSolid)
					image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\map\doorClosed.png", UriKind.Absolute));
				else if (IsDoor && !IsSolid)
					image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\map\doorOpened.png", UriKind.Absolute));
				else if (!IsSolid)
					image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\map\road.png", UriKind.Absolute));
				else
					image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\img\map\other.png", UriKind.Absolute));
			});
		}
	}
}
