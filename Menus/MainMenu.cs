using System.Collections.Generic;

namespace RLEngine.Menus
{
	public class MainMenu
	{
		static readonly List<string> mainMenuOptions = new List<string> {"Play Game", "Delete Save Game",
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
							int gameId = MenuProvider.CharacterSelectionMenu.SelectCharacterToPlay();

							if (gameId == -1)
								break;

							if (gameId == -2)
							{

								var newGameParameters = MenuProvider.CharacterCreationMenu.CreateNewCharacter();
								if (newGameParameters == null)
									break;

								newGameParameters.GameID = UserData.ApplicationSettings.GenerateNextGameId();

								var saveGameSummary = new UserData.SaveGameSummary(newGameParameters, "NEWGAME");
								UserDataManager.WriteSaveGameSummary(saveGameSummary);

								return newGameParameters.GameID;
							}

							return gameId;
						}
					case 1:
						{
							MenuProvider.CharacterSelectionMenu.DeleteSaveGame();
							break;
						}
					case 4:
						{
							MenuProvider.ViewKeysDisplay.ViewKeys();
							break;
						}
					case 5:
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
