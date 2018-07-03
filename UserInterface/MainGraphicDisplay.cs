// Tidied up for version 0.2.

using RLNET;
using RLEngine.UserInterface;
using RLEngine.Resources.Palette;

namespace RLEngine
{
    public static class MainGraphicDisplay
    {
		// TODO: Eventually these should probably go in a settings file or similar.
		static readonly int WINDOWWIDTH = 160;
		static readonly int WINDOWHEIGHT = 80;
		static readonly BackConsole _backConsole = new BackConsole(WINDOWWIDTH, WINDOWHEIGHT);

		static readonly int MENUOFFSET = 2;
		static readonly int MENUCONSOLEWIDTH = WINDOWWIDTH - 2 * MENUOFFSET;
		static readonly int MENUCONSOLEHEIGHT = WINDOWHEIGHT - 2 * MENUOFFSET;
		static readonly RLColor MENUCONSOLEBACKCOLOR = Palette.GetColor("Silver");
		static readonly MenuConsole _menuConsole = new MenuConsole(MENUCONSOLEWIDTH, MENUCONSOLEHEIGHT, MENUOFFSET, 
		                                                           MENUOFFSET, MENUCONSOLEBACKCOLOR, _backConsole);
		static readonly int MAPCONSOLEHEIGHT = WINDOWHEIGHT;
		static readonly int MAPCONSOLEWIDTH = MAPCONSOLEHEIGHT;
		static readonly int MAPCONSOLEOFFSET = 0;
		static readonly RLColor MAPCONSOLEBACKCOLOR = Palette.GetColor("Black");
		static readonly MapConsole _mapConsole = new MapConsole(MAPCONSOLEWIDTH, MAPCONSOLEHEIGHT, MAPCONSOLEOFFSET, 
		                                               MAPCONSOLEOFFSET, MAPCONSOLEBACKCOLOR, _backConsole);

		static readonly int TEXTCONSOLEWIDTH = 40;
		static readonly int TEXTCONSOLEHEIGHT = WINDOWHEIGHT;
		static readonly int TEXTCONSOLEXOFFSET = MAPCONSOLEWIDTH + 40;
		static readonly int TEXTCONSOLEYOFFSET = 0;
		static readonly RLColor TEXTCONSOLEBACKCOLOR = Palette.GetColor("Black");
		static readonly TextConsole _textConsole = new TextConsole(TEXTCONSOLEWIDTH, TEXTCONSOLEHEIGHT, 
		                                                           TEXTCONSOLEXOFFSET, TEXTCONSOLEYOFFSET, 
		                                                           TEXTCONSOLEBACKCOLOR, _backConsole);

		static readonly int CHARACTERCONSOLEWIDTH = 40;
		static readonly int CHARACTERCONSOLEHEIGHT = WINDOWHEIGHT;
		static readonly int CHARACTERCONSOLEXOFFSET = MAPCONSOLEWIDTH;
		static readonly int CHARACTERCONSOLEYOFFSET = 0;
		static readonly RLColor CHARACTERCONSOLEBACKCOLOR = Palette.GetColor("Black");
		static readonly CharacterConsole _characterConsole = new CharacterConsole(CHARACTERCONSOLEWIDTH,
																				  CHARACTERCONSOLEHEIGHT,
																				  CHARACTERCONSOLEXOFFSET,
																				  CHARACTERCONSOLEYOFFSET,
																				  CHARACTERCONSOLEBACKCOLOR,
																				  _backConsole);

		public static bool IsDirty
		{
			get
			{
				lock (_backConsole)
				{
					return _backConsole.IsDirty;
				}
			}
		}

		public static RLRootConsole CopyDisplayToRootConsole(RLRootConsole destination)
		{
			lock (_backConsole)
			{
				RLConsole.Blit(_backConsole, 0, 0, WINDOWWIDTH, WINDOWHEIGHT, destination, 0, 0);
				_backConsole.SetClean();
			}
			return destination;
		}

		public static MenuConsole MenuConsole
		{
			get { return _menuConsole; }
		}

		public static MapConsole MapConsole
		{
			get { return _mapConsole; }
		}

		public static TextConsole TextConsole
		{
			get { return _textConsole; }
		}

		public static CharacterConsole CharacterConsole
		{
			get { return _characterConsole; }
		}

		public static void UpdateGameScreen()
		{
			_textConsole.DrawOutputText();
			_mapConsole.DrawMap();
			_characterConsole.DrawCharacter();
		}
    }
}
