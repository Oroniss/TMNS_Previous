using RLEngine.Menus;

namespace RLEngine
{
	public static class MenuProvider
	{
		static MainMenu mainMenu;
		static ViewKeysDisplay viewKeys;

		public static MainMenu MainMenu
		{
			get {
				if (mainMenu == null)
					mainMenu = new MainMenu();
				return mainMenu;
			}
		}

		public static ViewKeysDisplay ViewKeysDisplay
		{
			get {
				if (viewKeys == null)
					viewKeys = new ViewKeysDisplay();
				return viewKeys;
			}
		}
	}
}
