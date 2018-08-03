using System.Collections.Generic;
using NUnit.Framework;
using TMNS.Menus;
using TMNS.UserData;
using TMNS.UserInterface;

namespace TMNS
{
	[TestFixture]
	public class CharacterSelectionTests
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
		public void TestCharacterSelectionBasic()
		{
			for (int i = 1; i< 4; i++)
			{
				var summary = new SaveGameSummary(new GameData(i, "Test" + i.ToString()), "TestLevel1");
				ApplicationSettings.AddSaveGame(i);
				UserDataManager.WriteSaveGameSummary(summary);
			}

			var keyList = new List<string> {"1"};

			UserInputHandler.AddKeyboardInput(keyList);
			var characterSelectionMenu = new CharacterSelectionMenu();

			Assert.AreEqual(1, characterSelectionMenu.SelectCharacterToPlay());
		}

		[Test]
		public void TestCharacterSelectionAdvanced()
		{
			for (int i = 1; i < 21; i++)
			{
				var summary = new SaveGameSummary(new GameData(i, "Test" + i.ToString()), "TestLevel1");
				ApplicationSettings.AddSaveGame(i);
				UserDataManager.WriteSaveGameSummary(summary);
			}

			var keyList = new List<string> { "RIGHT", "5" };

			UserInputHandler.AddKeyboardInput(keyList);
			var characterSelectionMenu = new CharacterSelectionMenu();

			Assert.AreEqual(15, characterSelectionMenu.SelectCharacterToPlay());
		}

		[Test]
		public void TestCharacterSelectionInvalid()
		{
			for (int i = 1; i < 15; i++)
			{
				var summary = new SaveGameSummary(new GameData(i, "Test" + i.ToString()), "TestLevel1");
				ApplicationSettings.AddSaveGame(i);
				UserDataManager.WriteSaveGameSummary(summary);
			}

			var keyList = new List<string> { "RIGHT", "8", "LEFT", "4" };

			UserInputHandler.AddKeyboardInput(keyList);
			var characterSelectionMenu = new CharacterSelectionMenu();

			Assert.AreEqual(4, characterSelectionMenu.SelectCharacterToPlay());
		}

		[Test]
		public void TestCharacterSelectionNew()
		{
			for (int i = 1; i < 4; i++)
			{
				var summary = new SaveGameSummary(new GameData(i, "Test" + i.ToString()), "TestLevel1");
				ApplicationSettings.AddSaveGame(i);
				UserDataManager.WriteSaveGameSummary(summary);
			}

			var keyList = new List<string> { "4" };

			UserInputHandler.AddKeyboardInput(keyList);
			var characterSelectionMenu = new CharacterSelectionMenu();

			Assert.AreEqual(-2, characterSelectionMenu.SelectCharacterToPlay());
		
		}

		[Test]
		public void TestCharacterSelectionCancel()
		{
			for (int i = 1; i < 4; i++)
			{
				var summary = new SaveGameSummary(new GameData(i, "Test" + i.ToString()), "TestLevel1");
				ApplicationSettings.AddSaveGame(i);
				UserDataManager.WriteSaveGameSummary(summary);
			}

			var keyList = new List<string> { "ESCAPE" };

			UserInputHandler.AddKeyboardInput(keyList);
			var characterSelectionMenu = new CharacterSelectionMenu();

			Assert.AreEqual(-1, characterSelectionMenu.SelectCharacterToPlay());
		}
	}
}
