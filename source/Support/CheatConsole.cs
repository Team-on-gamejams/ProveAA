using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	static class CheatConsole {
		const byte longestCommand = 200;
		static StringBuilder input;
		static Dictionary<string, Command> commands;

		static CheatConsole() {
			input = new StringBuilder(longestCommand);
			commands = new Dictionary<string, Command>();
		}

		public delegate void Command();

		static public void AddToInput(char c) {
			input.Append(c);
			TryExecute();
			TruncInput();
		}
		static public void AddToInput(string s) {
			input.Append(s);
			TryExecute();
			TruncInput();
		}

		static public void AddCommand(string str, Command command) {
			commands.Add(str.ToLower(), command);
		}

		static public void CatchInput(System.Windows.Window window) {
			window.KeyDown += (a, b) => {
				if (System.Windows.Input.Key.A <= b.Key && b.Key <= System.Windows.Input.Key.Z)
					AddToInput(b.Key.ToString().ToLower()[0]);
				else if (System.Windows.Input.Key.D0 <= b.Key && b.Key <= System.Windows.Input.Key.D9) {
					AddToInput(b.Key.ToString()[1]);
				}
			};
		}

		static void TruncInput() {
			if(input.Length > longestCommand) {
				byte half = longestCommand / 2;
				for (int i = 0; i < half; ++i) 
					input[i] = input[i + half];
			}
		}

		static void TryExecute() {
			foreach (var command in commands) {
				if (input.ToString().Contains(command.Key)) {
					command.Value.Invoke();
					input.Clear();
					return;
				}
			}
		}

		
	}
}
