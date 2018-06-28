using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	static class WPFExtencions {
		static public void ReopenWindow(this System.Windows.Window window, System.Windows.Window windowToOpen) {
			windowToOpen.Top = window.Top;
			windowToOpen.Left = window.Left;
			windowToOpen.Width = window.Width;
			windowToOpen.Height = window.Height;
			windowToOpen.Show();
			window.Close();
		}
	}
}
