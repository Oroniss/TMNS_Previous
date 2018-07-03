// Tidied up for version 0.2.

using RLEngine.Menus;

namespace RLEngine
{
	public static class MenuProvider
	{
		static MainMenu mainMenu;
		static CharacterCreationMenu characterCreationMenu;
		static ViewKeysDisplay viewKeys;

		public static MainMenu MainMenu
		{
			get 
			{
				if (mainMenu == null)
					mainMenu = new MainMenu();
				return mainMenu;
			}
		}

		public static CharacterCreationMenu CharacterCreationMenu
		{
			get
			{
				if (characterCreationMenu == null)
					characterCreationMenu = new CharacterCreationMenu();
				return characterCreationMenu;
			}
		}

		public static ViewKeysDisplay ViewKeysDisplay
		{
			get 
			{
				if (viewKeys == null)
					viewKeys = new ViewKeysDisplay();
				return viewKeys;
			}
		}
	}
}
