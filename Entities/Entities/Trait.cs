using System;

namespace RLEngine.Entities
{
	[Serializable]
	public enum Trait
	{
		// Testing traits
		TestTrait1,
		TestTrait2,

		// Class traits
		Player,
		Actor,
		Minion,
		Monster,
		NPC,
		Furnishing,
		Item,
		MapTile,

		// Basic stuff
		CanWalk,
		CanFly,
		CanSwim,
		Immobile,
		BlockMove,
		BlockLOS,
		Immobilised,
		Blindsight,
		SeeInvisible,
		TrueSeeing,
		Incorporeal,

		// Immunities/defenses
		ImmuneToAcid,
		ImmuneToCold,
		ImmuneToElectricity,
		ImmuneToFire,
		ImmuneToDisease,
		ImmuneToPoison,
		ImmuneToMental,
		ImmuneToPhysical,

		// Elemental traits
		Air,
		Earth,
		Fire,
		Water,
		Cold,
		Lightning,
		Holy,
		Unholy,

		// Creature Types
		Abberation,
		Animal,
		Beast,
		Construct,
		Dragon,
		Elemental,
		Giant,
		Humanoid,
		Ooze,
		Outsider,
		Plant,
		Undead,

		// Creature Subtypes - Animals
		Ape,
		Bear,
		Beetle,
		Bird,
		Cat,
		Centipede,
		Dog,
		Elephant,
		Fish,
		Frog,
		Insect,
		Horse,
		Lizard,
		Moth,
		Rhinoceros,
		Scorpion,
		Shark,
		Snake,
		Spider,
		Squid,
		Whale,

		// Creature Subtypes - Construct
		Golem,
		Statue,

		// Creature Subtypes - Giant
		Ogre,
		Troll,

		// Creature Subtypes - Humanoid
		Dwarf,
		Elf,
		Gnome,
		Goblin,
		Halfling,
		Hobgoblin,
		Human,
		Lizardman,
		Kobold,
		Orc,
		Troglodyte,

		// Creature Subtypes - Outsider
		Angel,
		Daemon,
		Demon,
		Devil,
		Fey,
		Guardinal,
		Modron,
		Slaad,

		// Material Types
		Cloth,
		Metal,
		Stone,
		Wood,
	}
}
