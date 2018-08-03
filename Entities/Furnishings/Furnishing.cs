using System;
using System.Collections.Generic;

namespace TMNS.Entities.Furnishings
{
	[Serializable]
	public partial class Furnishing:Entity
	{
		static SortedDictionary<int, Furnishing> furnishings = new SortedDictionary<int, Furnishing>();

		static int currentMaxId = 1;
		static List<int> freeFurnishingIds = new List<int>();

		static readonly List<Trait> furnishingTraits = new List<Trait> {Trait.ImmuneToPoison };

		int _furnishingId;

		string _description;
		Material _material;

		string _moveOnFunction;
		string _moveOffFunction;
		string _interactionFunction;
		string _interactionTrap;

		public Furnishing(FurnishingDetails details, int xLoc, int yLoc, Dictionary<string, string> otherParameters)
			: base(details, xLoc, yLoc, otherParameters)
		{
			if (freeFurnishingIds.Count > 0)
			{
				_furnishingId = freeFurnishingIds[0];
				freeFurnishingIds.RemoveAt(0);
			}
			else
			{
				_furnishingId = currentMaxId;
				currentMaxId++;
			}

			_description = details.Description;
			_material = details.Material;

			// Sets up all the functions in here.
			if (furnishingSetupFunctions.ContainsKey(EntityName))
				furnishingSetupFunctions[EntityName](this, otherParameters);

			// Then anything that needs to be added on
			SetupExtraParameters(this, otherParameters);

			furnishings[_furnishingId] = this;
		}

		public int FurnishingId
		{
			get { return _furnishingId; }
		}

		public override string FGColorName
		{
			get
			{
				if (_interactionTrap != null && PlayerSpotted)
				{
					return "Red";
				}
				return base.FGColorName;
			}
		}

		public override string GetDescription()
		{
			return _description;
		}

		public Material Material
		{
			get { return _material; }
		}

		public string MoveOnFunctionName
		{
			get { return _moveOnFunction; }
			set { _moveOnFunction = value; }
		}

		public bool MoveOn(Actors.Actor actor, int originX, int originY)
		{
			if (_moveOnFunction == null)
				return true;

			// TODO: Implement move function here.
			return true;
		}

		public string MoveOffFunctionName
		{
			get { return _moveOffFunction; }
			set { _moveOffFunction = value; }
		}

		public bool MoveOff(Actors.Actor actor, int destinationX, int destinationY)
		{
			if (_moveOffFunction == null)
				return true;

			// TODO: Implement move function here
			return true;
		}

		public string InteractionFunctionName
		{
			get { return _interactionFunction; }
			set { _interactionFunction = value; }
		}

		public string InteractionTrapName
		{
			get { return _interactionTrap; }
			set { _interactionTrap = value; }
		}

		public void InteractWith(Actors.Actor actor)
		{
			if (!PlayerSpotted && actor.HasTrait(Trait.Player))
			{
				MainGraphicDisplay.TextConsole.AddOutputText("There is nothing there to make use of");
				return;
			}

			if (_interactionFunction == null && _interactionTrap == null)
			{
				if (actor.HasTrait(Trait.Player))
				{
					MainGraphicDisplay.TextConsole.AddOutputText("You can't do anything with that");
				}
				else
				{
					// TODO: Add error text here
				}
				return;
			}

			if (_interactionTrap != null)
				interactionFunctions[_interactionTrap](this, actor);
			if (_interactionFunction != null)
				interactionFunctions[_interactionFunction](this, actor);
		}

		public override void Dispose()
		{
			base.Dispose();

			furnishings.Remove(_furnishingId);
			freeFurnishingIds.Add(_furnishingId);
		}

		public static Furnishing GetFurnishing(int furnishingId)
		{
			if (furnishings.ContainsKey(furnishingId))
				return furnishings[furnishingId];

			ErrorLogger.AddDebugText(string.Format("Tried to get unknown furnishing id: {0}", furnishingId));
			return null;
		}

		public static void UpdateFurnishings(Levels.Level currentLevel)
		{
			foreach (KeyValuePair<int, Furnishing> furnishing in furnishings)
				furnishing.Value.Update(currentLevel);
		}

		public static FurnishingSaveDetails GetSaveData()
		{
			var data = new FurnishingSaveDetails();

			data.CurrentMaxId = currentMaxId;
			data.FreeFurnishingIds = freeFurnishingIds;
			data.CurrentFurnishings = furnishings;

			return data;
		}

		public static void LoadSaveData(FurnishingSaveDetails data)
		{
			currentMaxId = data.CurrentMaxId;
			freeFurnishingIds = data.FreeFurnishingIds;
			furnishings = data.CurrentFurnishings;
		}

		public static void ClearFurnishingIds()
		{
			currentMaxId = 1;
			freeFurnishingIds = new List<int>();
			furnishings = new SortedDictionary<int, Furnishing>();
		}
	}
}
