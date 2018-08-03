using NUnit.Framework;
using TMNS.Menus;
using System.Collections.Generic;
using TMNS.UserInterface;
using TMNS.UserData;

namespace TMNS.Tests
{
	[TestFixture]
	public class CharacterCreationTests
	{
		static string _testFilePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");

		[SetUp]
		public void ClearSettings()
		{
			UserDataManager.SetTestHomeDirectory(_testFilePath);
			ApplicationSettings.ClearAllParameters();
			UserDataManager.DeleteSaveSummaryFile();
			UserDataManager.SetupDirectoriesAndFiles();
		}

		[TearDown]
		public void RemoveFiles()
		{
			UserDataManager.SetTestHomeDirectory(_testFilePath);
			ApplicationSettings.ClearAllParameters();
			UserDataManager.DeleteSaveSummaryFile();
		}

		[Test]
		public void TestCharacterCreationMenu()
		{
			var menu = new CharacterCreationMenu();

			var input = new List<string> { "F", "R", "E", "D", "ENTER"};
			UserInputHandler.AddKeyboardInput(input);

			var result = menu.CreateNewCharacter();
			Assert.AreEqual("Fred", result.GameIdentifier);
			Assert.AreEqual(1, result.GameID);

			input = new List<string> {"ESCAPE" };
			UserInputHandler.AddKeyboardInput(input);

			result = menu.CreateNewCharacter();
			Assert.IsNull(result);
		}
	}
}
