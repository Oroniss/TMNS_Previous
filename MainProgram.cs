using RLNET;

using System;
using System.Threading;
using System.Collections.Generic;

using TMNS.UserInterface;
using TMNS.Resources.RNG;

namespace TMNS
{
	public static class MainProgram
	{
		const string EngineVersion = "Version 0.1.1";

		static readonly string _fontName = "terminal8x8.png";
		static readonly int _consoleWidth = 160;
		static readonly int _consoleHeight = 80;
		static readonly int _fontSize = 8;
		static readonly float _scale = 1.0f;
		static readonly string _windowTitle = "The Mausoleum of Nightscale";

		static RLRootConsole rootConsole;

		// Level dictionary
		static Dictionary<Levels.LevelId, Levels.Level> _levels = new Dictionary<Levels.LevelId, Levels.Level>();

		// Global attributes
		static UserData.GameData _gameData;
		static int _currentTime;
		static Levels.Level _currentLevel;
		static Entities.Player.Player _player;
		static bool _quit;

		// RNG related attributes
		static RandomNumberGenerator _topLevelRNG;
		static RandomNumberGenerator _combatRNG;
		static RandomNumberGenerator _lootRNG;
		static RandomNumberGenerator _miscRNG;

		// TODO: Set this to the actual first level.
		static Levels.LevelId _startingLevel = Levels.LevelId.Level2A;
		static int _startingXLoc = 24;
		static int _startingYLoc = 53;

		public static void Main()
		{
			// Uncomment to clear settings if testing or something has broken things.

			//UserDataManager.DeleteSaveSummaryFile();
			//UserData.ApplicationSettings.ClearAllParameters();

			StaticDatabase.StaticDatabaseConnection.OpenDBConnection();
			UserDataManager.SetupDirectoriesAndFiles();

			UserInputHandler.ExtraKeys = UserData.ApplicationSettings.ExtraKeys;
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
				LevelTransition(_startingLevel, _startingXLoc, _startingYLoc);

			}
			else
			{
				LoadGame(gameState);
				// Have to complete the player's turn and increment timer.
				// TODO: Fix this up properly.
				Entities.Player.Player.UpdatePlayer(_currentLevel);
				_currentTime++;
			}

			RunGame();
			Quit();
		}

		static void RunGame()
		{
			while (true)
			{
				MainGraphicDisplay.UpdateGameScreen();

				// TODO: Swap the timer around.

				Entities.Monsters.Monster.UpdateMonsters(_currentLevel);
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
			_topLevelRNG = new RandomNumberGenerator(newGameData.GameID);
			_combatRNG = new RandomNumberGenerator(_topLevelRNG.GetRandomInteger());
			_lootRNG = new RandomNumberGenerator(_topLevelRNG.GetRandomInteger());
			_miscRNG = new RandomNumberGenerator(_topLevelRNG.GetRandomInteger());

			_player = new Entities.Player.Player(newGameData);
			_gameData = newGameData;

			// TODO: Create achievement Dictionary etc here.
			Quests.GameEventManager.SetupGameEventHandling();
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
			saveGameState.Monsters = Entities.Monsters.Monster.GetSaveData();

			Quests.GameEventManager.SaveData(saveGameState);

			saveGameState.TopLevelRNG = _topLevelRNG.GetSaveData();
			saveGameState.CombatRNG = _combatRNG.GetSaveData();
			saveGameState.LootRNG = _lootRNG.GetSaveData();
			saveGameState.MiscRNG = _miscRNG.GetSaveData();

			UserDataManager.SaveGame(saveGameState);
		}

		public static void LoadGame(UserData.SaveGame gameState)
		{
			_topLevelRNG = new RandomNumberGenerator(gameState.TopLevelRNG);
			_combatRNG = new RandomNumberGenerator(gameState.CombatRNG);
			_lootRNG = new RandomNumberGenerator(gameState.LootRNG);
			_miscRNG = new RandomNumberGenerator(gameState.MiscRNG);

			_gameData = gameState.Summary.GameData;
			_currentTime = gameState.CurrentTime;
			_player = gameState.Player;
			Entities.Player.Player.SetPlayer(_player);

			Entities.Furnishings.Furnishing.LoadSaveData(gameState.Furnishings);
			Entities.Monsters.Monster.LoadSaveData(gameState.Monsters);

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

		// Random number generation
		// TODO: Decide wether the top level one should be public as well?
		public static RandomNumberGenerator CombatRNG
		{
			get { return _combatRNG; }
		}

		public static RandomNumberGenerator LootRNG
		{
			get { return _lootRNG; }
		}

		public static RandomNumberGenerator MiscRNG
		{
			get { return _miscRNG; }
		}
	}
}
