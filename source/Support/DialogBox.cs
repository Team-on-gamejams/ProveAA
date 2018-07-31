using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	static class DialogBox {
		static public Windows.GameWindow window;

		static public byte choose;
		static public bool isChoose;

		static public void Init() {
			isChoose = true;
			window.dialogBtn1.Click += (a, b) => {
				//System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
					choose = 0;
					isChoose = true;
				//});
			};
			window.dialogBtn2.Click += (a, b) => {
				choose = 1;
				isChoose = true;
			};
			window.dialogBtn3.Click += (a, b) => {
				choose = 2;
				isChoose = true;
			};
			window.dialogBtn4.Click += (a, b) => {
				choose = 3;
				isChoose = true;
			};
			window.dialogBtn5.Click += (a, b) => {
				choose = 4;
				isChoose = true;
			};

		}

		static public void ChangeToDialog(string dialogText, string btnText1, string btnText2, string btnText3, string btnText4, string btnText5) {
			isChoose = false;

			window.dialogText.Text = dialogText;
			window.dialogBtn1.Content = btnText1;
			window.dialogBtn2.Content = btnText2;
			window.dialogBtn3.Content = btnText3;
			window.dialogBtn4.Content = btnText4;
			window.dialogBtn5.Content = btnText5;

			window.dialogBtn1.Opacity = !(btnText1?.Equals("") ?? true) ? 1 : 0;
			window.dialogBtn2.Opacity = !(btnText2?.Equals("") ?? true) ? 1 : 0;
			window.dialogBtn3.Opacity = !(btnText3?.Equals("") ?? true) ? 1 : 0;
			window.dialogBtn4.Opacity = !(btnText4?.Equals("") ?? true) ? 1 : 0;
			window.dialogBtn5.Opacity = !(btnText5?.Equals("") ?? true) ? 1 : 0;

			window.dialogBtn1.IsEnabled = !(btnText1?.Equals("") ?? true);
			window.dialogBtn2.IsEnabled = !(btnText2?.Equals("") ?? true);
			window.dialogBtn3.IsEnabled = !(btnText3?.Equals("") ?? true);
			window.dialogBtn4.IsEnabled = !(btnText4?.Equals("") ?? true);
			window.dialogBtn5.IsEnabled = !(btnText5?.Equals("") ?? true);

			System.Windows.Controls.Grid.SetZIndex(window.DialogBox, 4);
			window.DialogBox.Opacity = 1;
			window.MazeGrid.Opacity = 0.3;
		}

		static public void ReopenIfNeed() {
			if (!isChoose) {
				System.Windows.Controls.Grid.SetZIndex(window.DialogBox, 4);
				window.DialogBox.Opacity = 1;
				window.MazeGrid.Opacity = 0.3;
			}
		}
	}
}
