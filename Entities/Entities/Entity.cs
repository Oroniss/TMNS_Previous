using System;
using System.Collections.Generic;
using TMNS.Entities.EntityInterfaces;

namespace TMNS.Entities
{
	[Serializable]
	public abstract partial class Entity:ITrait
	{
		string _entityName;

		protected int _xLoc;
		protected int _yLoc;

		string _fgColorName;
		char _symbol;
		List<Trait> _traits;

		bool _isConcealed;
		bool _playerSpotted;
		int _spotDC;

		protected bool _destroyed;

		List<int> _effects;
		Dictionary<string, string> _otherAttributes;

		protected Entity(EntityBasicDetails details, int xLoc, int yLoc, Dictionary<string, string> otherParameters)
		{
			_entityName = details.EntityName;
			_xLoc = xLoc;
			_yLoc = yLoc;

			_traits = new List<Trait>();

			_effects = new List<int>();
			_otherAttributes = new Dictionary<string, string>();

			_destroyed = false;
			_playerSpotted = true;
			_spotDC = 0;

			// TODO: Set variables here
			_fgColorName = details.FGColorName;
			_symbol = details.Symbol;

			foreach (Trait trait in details.Traits)
				AddTrait(trait);

			SetupExtraParameters(this, otherParameters);
		}

		public string EntityName
		{
			get { return _entityName; }
		}

		public virtual string FGColorName
		{
			get { return _fgColorName; }
		}

		public char Symbol
		{
			get
			{
				if (!_playerSpotted)
					return ' ';
				return _symbol;
			}
			set { _symbol = value; }
		}

		public bool Concealed
		{
			get { return _isConcealed; }
			set { _isConcealed = value; }
		}

		public bool PlayerSpotted
		{
			get { return _playerSpotted; }
			set { _playerSpotted = value; }
		}

		public int SpotDC
		{
			get { return _spotDC; }
			set { _spotDC = value; }
		}

		public override string ToString()
		{
			return _entityName;
		}

		public virtual string GetDescription()
		{
			return _entityName;
		}

		public int XLoc
		{
			get { return _xLoc; }
		}

		public int YLoc
		{
			get { return _yLoc; }
		}

		public void AddTrait(Trait trait)
		{
			_traits.Add(trait);
		}

		public void RemoveTrait(Trait trait)
		{
			if (_traits.Contains(trait))
				_traits.Remove(trait);
			else
				ErrorLogger.AddDebugText(string.Format("Tried to remove non-existant trait from entity. " +
													   "Entity: {0}, Trait: {1}", this, trait));
		}

		public bool HasTrait(Trait trait)
		{
			return _traits.Contains(trait);
		}

		public bool HasOtherAttribute(string attributeName)
		{
			return _otherAttributes.ContainsKey(attributeName);
		}

		public void SetOtherAttribute(string attributeName, string attributeValue)
		{
			if (attributeValue == null)
			{
				if (_otherAttributes.ContainsKey(attributeName))
					_otherAttributes.Remove(attributeName);
			}
			else
				_otherAttributes[attributeName] = attributeValue;
		}

		public string GetOtherAttributeValue(string attributeName)
		{
			if (_otherAttributes.ContainsKey(attributeName))
				return _otherAttributes[attributeName];
			ErrorLogger.AddDebugText(string.Format("Tried to get unknown attribute value {0} on entity {1}",
												   attributeName, this));
			return "";
		}

		public virtual void Update(Levels.Level currentLevel)
		{
			// TODO: Go through effects and check if any expire.
		}

		public virtual void Dispose()
		{
			// TODO: Add effect removal in here.
			// TODO: Also any other stuff that comes up.
		}

		protected virtual void Destroy(Entity attacker)
		{
			if (!_destroyed)
			{
				// TODO: Create a Destroy entity event here.
			}
			_destroyed = true;
		}

		// TODO: Add a Destroy method with an effect as the attacker.
	}
}
