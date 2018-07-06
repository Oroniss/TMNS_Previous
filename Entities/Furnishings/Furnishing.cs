using System;
using System.Collections.Generic;

namespace RLEngine.Entities.Furnishings
{
	[Serializable]
	public partial class Furnishing:Entity
	{
		static SortedDictionary<int, Furnishing> furnishings = new SortedDictionary<int, Furnishing>();

		static int currentMaxId = 1;
		static List<int> freeFurnishingIds = new List<int>();

		static readonly List<Trait> furnishingTraits = new List<Trait> {Trait.ImmuneToPoison };

		int _furnishingId;
		bool _trapped;

		string _moveOnFunction;
		string _moveOffFunction;
		string _interactionFunction;

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

			// Sets up all the functions in here.
			if (furnishingSetupFunctions.ContainsKey(EntityName))
				furnishingSetupFunctions[EntityName](this, otherParameters);
			else
				DefaultFurnishingSetup(this, otherParameters);

			furnishings[_furnishingId] = this;
		}

		public int FurnishingId
		{
			get { return _furnishingId; }
		}

		public bool IsTrapped
		{
			get { return _trapped; }
			set { _trapped = value; }
		}

		public bool MoveOn(Actors.Actor actor, int originX, int originY)
		{
			if (_moveOnFunction == "")
				return true;

			// TODO: Implement move function here.
			return true;
		}

		public bool MoveOff(Actors.Actor actor, int destinationX, int destinationY)
		{
			if (_moveOffFunction == "")
				return true;

			// TODO: Implement move function here
			return true;
		}

		public bool InteractWith(Actors.Actor actor)
		{
			if (_interactionFunction == "No Use")
			{
				if (actor.HasTrait(Trait.Player))
				{
					MainGraphicDisplay.TextConsole.AddOutputText("You can't do anything with that");
					return true;
				}
				else
				{
					// TODO: Add error text here
				}
				
			}

			// TODO: Add function here.
			return false;
				
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
	}
}
