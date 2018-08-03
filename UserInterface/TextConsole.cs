using RLNET;
using System.Collections.Generic;
using TMNS.Resources.Palette;

namespace TMNS.UserInterface
{
	public class TextConsole:BaseConsole
	{
		readonly string[] _outputText;
		readonly string[] _textColors;
		int _currentIndex;

		readonly int _textXOffset = 1; // 1 Blank on the left
		readonly int _textWidth;
		readonly int _textYOffset = 3; // 1 Blank, 1 header, 1 more blank.
		readonly int _textHeight;
		const int _ARRAYLENGTH = 100; // TODO: Put this into a config file somewhere.

		public TextConsole(int width, int height, int left, int top, RLColor backColor, BackConsole backConsole)
			:base(width, height, left, top, backColor, backConsole)
		{
			_textWidth = _console.Width - 2; // 1 on each side
			_textHeight = _console.Height - 4; // 3 at the top, 1 at the bottom.

			_currentIndex = 0;
			_outputText = new string[_ARRAYLENGTH];
			_textColors = new string[_ARRAYLENGTH];
			for (var i = 0; i < _outputText.Length; i++)
			{
				_outputText[i] = "";
				_textColors[i] = "White";
			}
		}

		public void AddOutputText(string text)
		{
			AddOutputText(text, "White");
		}

		public void AddOutputText(string text, string textColor)
		{
			// Don't want two blank lines in a row.
			if (MessageIsEmpty(text) && LastMessageIsEmpty())
				return;

			if (text.Contains("\n"))
			{
				var splitText = text.Split('\n');
				for (var i = splitText.Length - 1; i >= 0; i--)
					AddOutputText(splitText[i], textColor);
				return;
			}

			if (text.Length > _textWidth)
			{
				var splitText = WrapText(text);
				for (var i = splitText.Count - 1; i >= 0; i--)
					AddOutputText(splitText[i], textColor);
				return;
			}

			_outputText[_currentIndex] = text;
			_textColors[_currentIndex] = textColor;
			_currentIndex = (_currentIndex + 1) % _ARRAYLENGTH;

			DrawOutputText();
		}

		List<string> WrapText(string Text)
		{
			var lines = new List<string>();
			while (true)
			{
				if (Text.Length <= _textWidth)
				{
					lines.Add(Text);
					return lines;
				}

				var lastSpace = Text.Substring(0, _textWidth).LastIndexOf(' ');
				if (lastSpace == -1)
				{
					ErrorLogger.AddDebugText("Message text too long without a space character.");
					return lines;
				}
				lines.Add(Text.Substring(0, lastSpace));
				Text = Text.Substring(lastSpace + 1);
			}
		}

		bool LastMessageIsEmpty()
		{
			return _outputText[(_currentIndex + (_ARRAYLENGTH - 1)) % _ARRAYLENGTH] == "";
		}

		bool MessageIsEmpty(string message)
		{
			return message == "";
		}

		public void DrawOutputText()
		{
			Clear();

			var messageLogTitle = string.Format("Message Log - Current Turn: {0}", MainProgram.CurrentTime);
			_console.Print(_textXOffset + ((_textWidth - messageLogTitle.Length) / 2), 1, 
			               messageLogTitle, Palette.GetColor("White"));

			for (int i = 0; i < _textHeight; i++)
			{
				int textIndex = (_currentIndex - 1 - i + _ARRAYLENGTH) % _ARRAYLENGTH;
				_console.Print(_textXOffset, _textYOffset + i, _outputText[textIndex], 
				               Palette.GetColor(_textColors[textIndex]));
			}
			CopyToBackConsole();
		}
	}
}
