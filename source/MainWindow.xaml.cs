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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProveAA {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();

		}

		private void Window_Initialized(object sender, EventArgs e) {
			GameWindow gameWindow = new GameWindow();
			gameWindow.Top = this.Top;
			gameWindow.Left = this.Left;
			gameWindow.Width = this.Width;
			gameWindow.Height = this.Height;
			gameWindow.Show();
			this.Close();
		}
	}
}
