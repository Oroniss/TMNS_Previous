using TMNS.UserData;

namespace TMNS.Menus
{
	public class CharacterCreationMenu
	{
		public GameData CreateNewCharacter()
		{
			var parameters = new GameData();

			parameters = GetCharacterName(parameters);

			// TODO: Get other pieces here too.

			if (parameters == null)
				return parameters;

			parameters.GameID = ApplicationSettings.GenerateNextGameId();

			var saveGameSummary = new SaveGameSummary(parameters, "NEWGAME");
			UserDataManager.WriteSaveGameSummary(saveGameSummary);

			return parameters;
		}

		GameData GetCharacterName(GameData parameters)
		{
			var characterNote = UserInterface.UserInputHandler.GetText("What is your name?");
			if (characterNote == null)
				return null;
			parameters.GameIdentifier = characterNote;
			return parameters;
		}
	}
}
