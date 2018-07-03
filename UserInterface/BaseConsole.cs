// Tidied up for version 0.2.

using RLNET;

namespace RLEngine.UserInterface
{
	public abstract class BaseConsole
	{
		readonly int _height;
		readonly int _width;
		readonly int _top;
		readonly int _left;
		readonly RLColor _backColor;
		readonly BackConsole _backConsole;
		protected RLConsole _console;

		protected BaseConsole(int width, int height, int left, int top, RLColor backColor, BackConsole backConsole)
		{
			_height = height;
			_width = width;
			_left = left;
			_top = top;
			_backConsole = backConsole;
			_backColor = backColor;

			_console = new RLConsole(_width, _height);
			_console.SetBackColor(0, 0, _width, _height, backColor);
		}

		protected void CopyToBackConsole()
		{
			lock (_backConsole)
			{
				RLConsole.Blit(_console, 0, 0, _width, _height, _backConsole, _left, _top);
				_backConsole.SetDirty();
			}
		}

		protected void Clear()
		{
			_console.Clear();
			_console.SetBackColor(0, 0, _width, _height, _backColor);
		}
	}
}
