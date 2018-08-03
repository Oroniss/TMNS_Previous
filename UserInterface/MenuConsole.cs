using RLNET;
using System.Collections.Generic;
using TMNS.Resources.Palette;

namespace TMNS.UserInterface
{
	public class MenuConsole : BaseConsole
	{
		const int TEXTBLANKLINES = 5;
		const int TEXTXOFFSET = 5;
		const int MAXTEXTSPACING = 5;

		public MenuConsole(int width, int height, int left, int top, RLColor backColor, BackConsole backConsole)
			: base(width, height, left, top, backColor, backConsole)
		{
		}


		public void DrawTextBlock(string title, List<string> options, string bottom)
		{
			Clear();

			if (options.Count > (_console.Height - TEXTBLANKLINES))
			{
				ErrorLogger.AddDebugText("Too many menu items for menu: " + title);
				return;
			}

			var lineSpacing = System.Math.Min(_console.Height / (options.Count + TEXTBLANKLINES), MAXTEXTSPACING);

			_console.Print(TEXTXOFFSET, lineSpacing, title, Palette.GetColor("Black"));

			for (var line = 0; line < options.Count; line++)
			{
				if (options[line].Contains("\n"))
				{
					var pieces = options[line].Split('\n');

					if (pieces.Length > lineSpacing)
						ErrorLogger.AddDebugText("Too many lines in menu item" + pieces);

					for (var linePiece = 0; linePiece < pieces.Length; linePiece++)
						_console.Print(TEXTXOFFSET, lineSpacing * (line + 3) + linePiece, pieces[linePiece], 
						               Palette.GetColor("Black"));
				}
				else
					_console.Print(TEXTXOFFSET, lineSpacing * (line + 3), options[line], Palette.GetColor("Black"));
			}

			_console.Print(TEXTXOFFSET, _console.Height - lineSpacing, bottom, Palette.GetColor("Black"));

			CopyToBackConsole();
		}
	}
}
