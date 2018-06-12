using RLEngine.UserData;

namespace RLEngine.Menus
{
	public class CharacterCreationMenu
	{

		public GameData CreateNewCharacter()
		{
			GameData parameters = new GameData();

			parameters = GetCharacterName(parameters);

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
