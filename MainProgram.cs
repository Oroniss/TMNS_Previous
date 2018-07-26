using System;
using System.Threading;
using RLEngine.UserInterface;
using RLNET;
using System.Collections.Generic;

namespace RLEngine
{
	public static class MainProgram
	{
		const string EngineVersion = "Version 0.3";

		// TODO: Put these into a config file somewhere.
		static readonly string _fontName = "terminal8x8.png";
		static readonly int _consoleWidth = 160;
		static readonly int _consoleHeight = 80;
		static readonly int _fontSize = 8;
		static readonly float _scale = 1.0f;
		static readonly string _windowTitle = "Halfbreed";

		static RLRootConsole rootConsole;

		// Level dictionary
		static Dictionary<Levels.LevelId, Levels.Level> _levels = new Dictionary<Levels.LevelId, Levels.Level>();

		// Global attributes
		static UserData.GameData _gameData;
		static int _currentTime;
		static Levels.Level _currentLevel;
		static Entities.Player.Player _player;
		static bool _quit;

		static Levels.LevelId _startingLevel = Levels.LevelId.TestLevel2;
		static int _startingXLoc = 2;
		static int _startingYLoc = 2;

		public static void Main()
		{
			// Uncomment to clear settings if testing or something has broken things.

			//UserDataManager.DeleteSaveSummaryFile();
			//UserData.ApplicationSettings.ClearAllParameters();

			StaticDatabase.StaticDatabaseConnection.OpenDBConnection();
			UserDataManager.SetupDirectoriesAndFiles();

			UserInputHandler.ExtraKeys = UserData.ApplicationSettings.ExtraKeys;
			UserDataManager.FullLogging = UserData.ApplicationSettings.FullLogging;
			// TODO: Set the gm option here too.

			rootConsole = new RLRootConsole(_fontName, _consoleWidth, _consoleHeight, _fontSize, _fontSize, _scale,
											_windowTitle);

			rootConsole.Update += RootConsoleUpdate;
            rootConsole.Render += RootConsoleRender;

			var mainLoopThread = new Thread(RunStartMenu);
			mainLoopThread.Start();
            rootConsole.Run();
			StaticDatabase.StaticDatabaseConnection.CloseDBConnection();
		}

		static void RootConsoleRender(object sender, EventArgs e)
		{
			if (MainGraphicDisplay.IsDirty)
			{
				rootConsole.Clear();
				rootConsole = MainGraphicDisplay.CopyDisplayToRootConsole(rootConsole);
				rootConsole.Draw();
			}
		}

		static void RootConsoleUpdate(object sender, EventArgs e)
		{
			var key = rootConsole.Keyboard.GetKeyPress();
			if (key != null)
			{
				UserInputHandler.AddKeyboardInput(key.Key);
			}
		}

		static void RunStartMenu()
		{
			var gameId = MenuProvider.MainMenu.DisplayMainMenu();
			if (gameId == -1)
			{
				Quit();
				return;
			}

			var gameState = UserDataManager.GetGameState(gameId);

			if (gameState.Summary.CurrentLevelName == "NEWGAME")
			{
				SetupNewGame(gameState.Summary.GameData);
				Quests.GameEventManager.SetupGameEventHandling();
				LevelTransition(_startingLevel, _startingXLoc, _startingYLoc);

			}
			else
			{
				LoadGame(gameState);
				// Have to complete the player's turn and increment timer.
				Entities.Player.Player.UpdatePlayer(_currentLevel);
				_currentTime++;
			}

			// TODO: This should eventually go above to either create new ones or deserialise the old ones.

			RunGame();
			Quit();
		}

		static void RunGame()
		{
			while (true)
			{
				MainGraphicDisplay.UpdateGameScreen();

				Entities.NPCs.Monster.UpdateMonsters(_currentLevel);
				Entities.Player.Player.UpdatePlayer(_currentLevel);

				if (_quit)
				{
					SaveGame();
					return;
				}
				_currentTime++;
			}
		}

		public static void Quit()
		{
			_quit = true;
			rootConsole.Close();
		}

		public static void LevelTransition(Levels.LevelId newLevelId, int newXLoc, int newYLoc)
		{
			if (_currentLevel != null)
			{
				_currentLevel.RemoveActor(_player);
				_currentLevel.Dispose();
				_levels.Remove(_currentLevel.LevelId);
			}

			var newLevel = new Levels.Level(newLevelId);
			_levels[newLevelId] = newLevel;
			_currentLevel = newLevel;

			_player.UpdatePosition(newXLoc, newYLoc);
			_currentLevel.AddActor(_player);
		}

		static void SetupNewGame(UserData.GameData newGameData)
		{
			// TODO: Check whether anything else needs to go in here too.
			_player = new Entities.Player.Player(newGameData);
			_gameData = newGameData;
		}

		public static void SaveGame()
		{
			var summary = new UserData.SaveGameSummary(_gameData, _currentLevel.LevelName);
			var saveGameState = new UserData.SaveGame(summary);

			saveGameState.CurrentTime = _currentTime;
			saveGameState.Player = _player;

			saveGameState.CurrentLevelId = _currentLevel.LevelId;
			foreach (KeyValuePair<Levels.LevelId, Levels.Level> level in _levels)
				saveGameState.Levels[level.Key] = level.Value.GetSaveDetails();

			saveGameState.Furnishings = Entities.Furnishings.Furnishing.GetSaveData();
			saveGameState.Monsters = Entities.NPCs.Monster.GetSaveData();

			Quests.GameEventManager.SaveData(saveGameState);

			UserDataManager.SaveGame(saveGameState);
		}

		public static void LoadGame(UserData.SaveGame gameState)
		{
			_gameData = gameState.Summary.GameData;
			_currentTime = gameState.CurrentTime;
			_player = gameState.Player;
			Entities.Player.Player.SetPlayer(_player);

			Entities.Furnishings.Furnishing.LoadSaveData(gameState.Furnishings);
			Entities.NPCs.Monster.LoadSaveData(gameState.Monsters);

			foreach (KeyValuePair<Levels.LevelId, Levels.LevelSaveSummary> level in gameState.Levels)
				_levels[level.Key] = new Levels.Level(level.Value);

			_currentLevel = _levels[gameState.CurrentLevelId];

			Quests.GameEventManager.LoadData(gameState);
		}

		public static int CurrentTime
		{
			get { return _currentTime; }
		}

		public static Levels.Level CurrentLevel
		{
			get { return _currentLevel; }
		}

		public static Entities.Player.Player Player
		{
			get { return _player; }
		}
	}
}
