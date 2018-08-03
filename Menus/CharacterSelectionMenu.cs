using System.Collections.Generic;
using TMNS.UserData;

namespace TMNS.Menus
{
	public class CharacterSelectionMenu
	{

		public void DeleteSaveGame()
		{
			var currentSummaries = GetCurrentSaveSummaries();
			var menuOptions = GetMenuOptions(currentSummaries);

			while (true)
			{
				int selection = UserInterface.UserInputHandler.SelectFromMenu(
					"Which save would you like to delete. This cannot be reversed",
					menuOptions, "Escape to exit");

				if (selection == -1)
					return;

				int saveToDelete = currentSummaries[selection].GameData.GameID;

				UserDataManager.DeleteSaveGame(saveToDelete);
				ApplicationSettings.RemoveSaveGame(saveToDelete);
				currentSummaries.RemoveAt(selection);
				menuOptions.RemoveAt(selection);
			}
		}

		public int SelectCharacterToPlay()
		{
			var currentSummaries = GetCurrentSaveSummaries();
			var menuOptions = GetMenuOptions(currentSummaries);

			if (ApplicationSettings.HasFreeSaveSlot())
				menuOptions.Add("Create New Character");

			int selection = UserInterface.UserInputHandler.SelectFromMenu("Select Character", menuOptions, "Escape to cancel");

			if (selection == -1)
				return -1;

			if (selection == currentSummaries.Count)
			{
				return -2;
			}

			return currentSummaries[selection].GameData.GameID;
		}

		List<SaveGameSummary> GetCurrentSaveSummaries()
		{
			var currentSaves = ApplicationSettings.GetSaveGameIds();

			List<SaveGameSummary> currentSummaries = new List<SaveGameSummary>();

			foreach (int i in currentSaves)
				currentSummaries.Add(UserDataManager.GetSummary(i));
			return currentSummaries;
		}

		List<string> GetMenuOptions(List<SaveGameSummary> currentSummaries)
		{
			List<string> menuOptions = new List<string>();
			foreach (SaveGameSummary summary in currentSummaries)
				menuOptions.Add(summary.ToString());
			return menuOptions;
		}
	}
}
