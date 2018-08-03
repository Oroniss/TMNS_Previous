using TMNS.Menus;

namespace TMNS
{
	public static class MenuProvider
	{
		static MainMenu mainMenu;
		static CharacterSelectionMenu characterSelectionMenu;
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

		public static CharacterSelectionMenu CharacterSelectionMenu
		{
			get
			{
				if (characterSelectionMenu == null)
					characterSelectionMenu = new CharacterSelectionMenu();
				return characterSelectionMenu;
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
