using System;
using System.Threading;
using System.Collections.Generic;
using RLEngine.Resources.Geometry;
using RLNET;

namespace RLEngine.UserInterface
{
    public static class UserInputHandler
    {
		const int MAXTEXTLENGTH = 40;

		static bool _extraKeys;

		static readonly Dictionary<RLKey, string> _keyDict = new Dictionary<RLKey, string>
		{
			{RLKey.Number0, "0"}, {RLKey.Number1, "1"}, {RLKey.Number2, "2"}, {RLKey.Number3, "3"}, 
			{RLKey.Number4, "4"}, {RLKey.Number5, "5"}, {RLKey.Number6, "6"}, {RLKey.Number7, "7"}, 
			{RLKey.Number8, "8"}, {RLKey.Number9, "9"},

			{RLKey.Left, "LEFT"}, {RLKey.Right, "RIGHT"}, {RLKey.Up, "UP"}, {RLKey.Down, "DOWN"},
			{RLKey.Keypad1, "DOWN_LEFT"}, {RLKey.Keypad2, "DOWN"}, {RLKey.Keypad3, "DOWN_RIGHT"}, 
			{RLKey.Keypad4, "LEFT"}, {RLKey.Keypad6, "RIGHT"}, {RLKey.Keypad7, "UP_LEFT"}, {RLKey.Keypad8, "UP"}, 
			{RLKey.Keypad9, "UP_RIGHT"},

			{RLKey.Escape, "ESCAPE"}, {RLKey.Space, "SPACE"}, {RLKey.Enter, "ENTER"}, {RLKey.BackSpace, "BACKSPACE"},
			{RLKey.Minus, "MINUS"}, {RLKey.Plus, "EQUALS"}, {RLKey.Tab, "TAB"}, {RLKey.Comma, "COMMA"},
			{RLKey.Period, "PERIOD"}, {RLKey.Slash, "SLASH"},

			{RLKey.A, "A"}, {RLKey.B, "B"}, {RLKey.C, "C"}, {RLKey.D, "D"}, {RLKey.E, "E"}, {RLKey.F, "F"},
			{RLKey.G, "G"}, {RLKey.H, "H"}, {RLKey.I, "I"}, {RLKey.J, "J"}, {RLKey.K, "K"}, {RLKey.L, "L"}, 
			{RLKey.M, "M"}, {RLKey.N, "N"}, {RLKey.O, "O"}, {RLKey.P, "P"}, {RLKey.Q, "Q"}, {RLKey.R, "R"},
			{RLKey.S, "S"}, {RLKey.T, "T"}, {RLKey.U, "U"}, {RLKey.V, "V"}, {RLKey.W, "W"}, {RLKey.X, "X"}, 
			{RLKey.Y, "Y"}, {RLKey.Z, "Z"}
		};

		public static readonly Dictionary<string, int> NumberKeys = new Dictionary<string, int>
			{{"0", 0}, {"1", 1},  {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5}, {"6", 6}, {"7", 7}, {"8", 8}, {"9", 9}};

		public static readonly Dictionary<string, XYCoordinateStruct> DirectionKeys = new Dictionary<string, XYCoordinateStruct>
			{{"UP", new XYCoordinateStruct(0, -1)}, {"DOWN", new XYCoordinateStruct(0, 1)}, 
			{"LEFT", new XYCoordinateStruct(-1, 0)}, {"RIGHT", new XYCoordinateStruct(1, 0)},
			{"UP_LEFT", new XYCoordinateStruct(-1, -1)}, {"UP_RIGHT", new XYCoordinateStruct(1, -1)}, 
			{"DOWN_LEFT", new XYCoordinateStruct(-1, 1)}, {"DOWN_RIGHT", new XYCoordinateStruct(1, 1)}};

		public static readonly HashSet<string> LetterKeys = new HashSet<string>
		{"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U",
		"V", "W", "X", "Y", "Z"};

		static List<string> _queuedInput = new List<string>();


		public static void addKeyboardInput(RLKey key)
        {
			if (_keyDict.ContainsKey(key))
			{
				lock (_queuedInput)
					_queuedInput.Add(_keyDict[key]);
			}
        }

		public static void addKeyboardInput(string key)
		{
			lock(_queuedInput)
				_queuedInput.Add(key);
		}

		public static void clearAllInput()
		{
			lock (_queuedInput)
				_queuedInput = new List<string>();
		}

        public static string getNextKey()
        {
            while (true)
            {
                lock (_queuedInput)
                {
                    if(_queuedInput.Count > 0)
                    {
                        var toReturn = _queuedInput[0];
						_queuedInput.RemoveAt(0);
                        return toReturn;
                    }
                }
                Thread.Yield();
            }
        }

		public static int SelectFromMenu(string title, List<string> menuOptions, string bottom)
		{
			int page = 0;
			while (true)
			{
				var currentDisplay = new List<string>();
				for (int i = 10 * page; i < Math.Min(10 * (page + 1), menuOptions.Count); i++)
				{
					currentDisplay.Add(((i % 10 + 1) % 10).ToString() + ": " + menuOptions[i]);
				}
				MainGraphicDisplay.MenuConsole.DrawTextBlock(title, currentDisplay, bottom);

				var key = getNextKey();

				if (NumberKeys.ContainsKey(key))
				{
					int num = NumberKeys[key];
					if (num == 0)
					{
						if ((page + 1) * 10 <= menuOptions.Count)
						{
							return (page + 1) * 10 - 1;
						}
					}
					else
					{
						if (num + page * 10 <= menuOptions.Count)
						{
							return num - 1 + page * 10;
						}
					}
				}
				if (key == "LEFT" && page > 0)
				{
					page--;
				}
				if (key == "RIGHT" && ((page + 1) * 10 < menuOptions.Count))
				{
					page++;
				}
				if (key == "ESCAPE")
				{
					return -1;
				}
			}
		}

		public static XYCoordinateClass GetDirection(string queryText, bool centre)
		{
			if (queryText == "")
				queryText = "Which direction?";

			while (true)
			{
				MainGraphicDisplay.TextConsole.AddOutputText(queryText);
				var key = getNextKey();

				if (DirectionKeys.ContainsKey(key))
					return new XYCoordinateClass(DirectionKeys[key].X, DirectionKeys[key].Y);
				if (key == "SPACE" && centre)
					return new XYCoordinateClass(0, 0);
				if (key == "ESCAPE")
					return null;
			}
		}

		public static XYCoordinateClass GetDirection()
		{
			return GetDirection("", true);
		}

		public static string GetText(string headerText)
		{
			var currentText = "";
			while (true)
			{
				MainGraphicDisplay.MenuConsole.DrawTextBlock(headerText, new List<string> { currentText },
														"Escape to cancel");
				var key = getNextKey();
				var converter = new System.Globalization.CultureInfo("en-US");

				if (LetterKeys.Contains(key) && currentText.Length <= MAXTEXTLENGTH)
				{
					currentText += key.ToLower(); // ToLower required since all caps strings are changed.
					currentText = converter.TextInfo.ToTitleCase(currentText);
				}
				if (key == "SPACE")
					currentText += " ";
				if (key == "BACKSPACE")
					currentText = currentText.Substring(0, currentText.Length - 1);
				if (key == "ESCAPE")
					return null;
				if (key == "ENTER")
					return currentText;
			}
		}
		// TODO: See if this is something that comes up enough to generalise.
		public static void DisplayConfigMenu()
		{
			
			var configParameters = UserDataManager.ReadConfigParameters();
			var logging = configParameters.FullLogging;
			var keys = configParameters.ExtraKeys;
			var gm = configParameters.GMOptions;

			var title = "Select Configuration Options";
			var options = new List<string> { "", "", "" };
			var bottom = "Escape to exit";

			while (true)
			{
				var loggingText = "Off";
				if (logging)
					loggingText = "On";
				options[0] = string.Format("L: Toggle full game log - currently {0}", loggingText);
				var keysText = "Off";
				if (keys)
					keysText = "On";
				options[1] = string.Format("K: Toggle laptop keys - currently {0}", keysText);
				var gmText = "Off";
				if (gm)
					gmText = "On";
				options[2] = string.Format("G: Toggle GM Options - currently {0}", gmText);

				MainGraphicDisplay.MenuConsole.DrawTextBlock(title, options, bottom);

				var key = getNextKey();

				if (key == "L")
					logging = !logging;
				if (key == "K")
					keys = !keys;
				if (key == "G")
					gm = !gm;
				if (key == "ESCAPE")
				{
					UserDataManager.WriteConfigFile(new UserData.ConfigParameters(keys, logging, gm));
					return;
				}
			}

		}

		public static bool ExtraKeys
		{
			get { return _extraKeys; }
			set {
				_extraKeys = value;
				if (value)
					AddExtraKeys();
				else
					RemoveExtraKeys();
			}
		}

		static void AddExtraKeys()
		{
			_keyDict[RLKey.Semicolon] = "DOWN_LEFT";
			_keyDict[RLKey.Quote] = "DOWN_RIGHT";
			_keyDict[RLKey.BracketLeft] = "UP_LEFT";
			_keyDict[RLKey.BracketRight] = "UP_RIGHT";
		}

		static void RemoveExtraKeys()
		{
			if (_keyDict.ContainsKey(RLKey.Semicolon))
				_keyDict.Remove(RLKey.Semicolon);
			if (_keyDict.ContainsKey(RLKey.Quote))
				_keyDict.Remove(RLKey.Quote);
			if (_keyDict.ContainsKey(RLKey.BracketLeft))
				_keyDict.Remove(RLKey.BracketLeft);
			if (_keyDict.ContainsKey(RLKey.BracketRight))
				_keyDict.Remove(RLKey.BracketRight);
		}
    }
}
