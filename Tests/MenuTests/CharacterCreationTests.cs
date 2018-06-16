// Finished for version 0.1.

using NUnit.Framework;
using RLEngine.Menus;
using System.Collections.Generic;
using RLEngine.UserInterface;

namespace RLEngine.Tests
{
	[TestFixture]
	public class CharacterCreationTests
	{
		[Test]
		public void TestCharacterCreationMenu()
		{
			var menu = new CharacterCreationMenu();

			var input = new List<string> { "F", "R", "E", "D", "ENTER"};
			UserInputHandler.AddKeyboardInput(input);

			var result = menu.CreateNewCharacter();
			Assert.AreEqual("Fred", result.GameIdentifier);
			Assert.AreEqual(0, result.GameID);

			input = new List<string> {"ESCAPE" };
			UserInputHandler.AddKeyboardInput(input);

			result = menu.CreateNewCharacter();
			Assert.IsNull(result);
		}
	}
}
