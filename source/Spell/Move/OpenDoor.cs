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

namespace ProveAA.Spell.Move {
	class OpenDoor : BasicSpell {
		public byte DoorId { get; set; }
		public byte DoorZoneStyleNum { get; set; }

		public OpenDoor(byte _DoorId, byte _doorZoneStyleNum) {
			DoorZoneStyleNum = _doorZoneStyleNum;
			this.DoorId = _DoorId;
			itemImgPath = @"img\map\" + DoorZoneStyleNum.ToString() + @"\openDoor_" + DoorId.ToString();
		}

		public override bool CardUsed(Creature.Player pl) {
			if (pl.IsInBattle)
				return false;

			Tuple<byte, byte> doorPos = null;
			for (byte i = 0; i < pl.Map.SizeY; ++i) {
				for (byte j = 0; j < pl.Map.SizeX; ++j) {
					if(pl.Map[i, j].IsDoor && pl.Map[i, j].DoorId == DoorId && !pl.Map[i, j].IsInFog) {
						doorPos = new Tuple<byte, byte>(i, j);
						break;
					}
				}
			}

			if(doorPos != null) {
				if(pl.Map[doorPos.Item1, doorPos.Item2].DoorId == 0) {
					Support.DialogBox.ChangeToDialog("You complete the level. Whats next?",
						"Continue explore",
						pl.Map.globalMap.CanMoveLeft? "Left" + pl.Map.globalMap.LeftFromPlayer.CellName : null,
						pl.Map.globalMap.CanMoveUp ? "Up" + pl.Map.globalMap.RightFromPlayer.CellName : null,
						pl.Map.globalMap.CanMoveRight ? "Right" + pl.Map.globalMap.UpFromPlayer.CellName : null,
						pl.Map.globalMap.CanMoveDown ? "Down" + pl.Map.globalMap.DownFromPlayer.CellName : null
					);

					System.Timers.Timer t = new System.Timers.Timer() {
						AutoReset = true,
						Enabled = false,
						Interval = 100,
					};

					t.Elapsed += (a, b) => {
						System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
						if (Support.DialogBox.isChoose) {
							byte choose = Support.DialogBox.choose;
							if (choose == 1)
								pl.Map.globalMap.MoveLeft();
							else if (choose == 2)
								pl.Map.globalMap.MoveUp();
							else if (choose == 3)
								pl.Map.globalMap.MoveRight();
							else if (choose == 4)
								pl.Map.globalMap.MoveDown();

							pl.Map.NewLevel(pl);
							pl.ChangeToMaze();
							pl.Map.globalMap.SetMarkerPos();
							t.Stop();
						}
						});
					};

					t.Start();

					return true;
				}
				else {
					pl.Map[doorPos.Item1, doorPos.Item2].IsDoorOpened = true;
					pl.PosX = doorPos.Item2;
					pl.PosY = doorPos.Item1;
					pl.PosChanged();
					return true;
				}
			}

			return false;
		}
	}
}
