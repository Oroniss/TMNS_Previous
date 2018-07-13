using System;
using System.Threading;
using RLEngine.UserInterface;
using RLNET;

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

		// Global attributes
		static int _currentTime;
		static Levels.Level _currentLevel;
		static Entities.Player.Player _player;
		static bool _quit;

		static Levels.LevelId _startingLevel = Levels.LevelId.TestLevel2;
		static int _startingXLoc = 2;
		static int _startingYLoc = 2;

		public static void Main()
		{
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
			// TODO: Update this bit.
			//else
			//	LoadGame(gameState);

			// TODO: This should eventually go above to either create new ones or deserialise the old ones.
			Quests.GameEventManager.Setup();


			RunGame();
			Quit();
		}

		static void RunGame()
		{
			while (true)
			{
				MainGraphicDisplay.UpdateGameScreen();

				Entities.Actors.Actor.UpdateActors(_currentLevel);
				Entities.Player.Player.UpdatePlayer(_currentLevel);

				if (_quit)
				{
					//SaveGame();
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

		public static void LevelTransition(Levels.LevelId newLevel, int newXLoc, int newYLoc)
		{
			if (_currentLevel != null)
			{
				_currentLevel.RemoveActor(_player);
				_currentLevel.Dispose();
			}

			_currentLevel = new Levels.Level(newLevel);
			_player.UpdatePosition(newXLoc, newYLoc);
			_currentLevel.AddActor(_player);
		}

		static void SetupNewGame(UserData.GameData newGameData)
		{
			// TODO: Check whether anything else needs to go in here too.
			_player = new Entities.Player.Player(newGameData);
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
