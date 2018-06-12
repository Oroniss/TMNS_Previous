using System.Collections.Generic;

namespace RLEngine.Menus
{
	public class MainMenu
	{
		static readonly List<string> mainMenuOptions = new List<string> {"New Game", "Load Game", "Delete Save Game",
			"View Achievements", "Clear Achievements", "View Commands", "Config"};

		public int DisplayMainMenu()
		{
			while (true)
			{
				
				var selection = UserInterface.UserInputHandler.SelectFromMenu("Welcome to Engine Test", mainMenuOptions, 
				                                                              "Escape to Quit");

				switch (selection)
				{
					case 0:
						{
							var newGameParameters = MenuProvider.CharacterCreationMenu.CreateNewCharacter();
							if (newGameParameters == null)
								break;

							newGameParameters.GameID = UserDataManager.GetNextGameId();

							var saveGameSummary = new UserData.SaveGameSummary(newGameParameters, "NEWGAME", true);
							UserDataManager.WriteSaveGameSummary(saveGameSummary);

							return newGameParameters.GameID;
						}
					case 1:
						{
							// TODO: Go to load character screen.
							break;
						}
					case 2:
						{
							// TODO: Delete save game.
							break;
						}
					case 5:
						{
							MenuProvider.ViewKeysDisplay.ViewKeys();
							break;
						}
					case 6:
						{
							UserInterface.UserInputHandler.DisplayConfigMenu();
							break;
						}
					case -1:
						{
							return -1;
						}
				}
			}
		}

	}
}
