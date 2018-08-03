using System;

namespace TMNS.Resources.RNG
{
	public class RandomNumberGenerator
	{
		const int MAXNUMBEROFUSES = 1000;

		int _currentSeed;
		Random _randomNumberGenerator;
		int _numberOfUses;

		public RandomNumberGenerator(int seed)
		{
			_currentSeed = seed;
			_randomNumberGenerator = new Random(seed);
		}

		public RandomNumberGenerator(RandomNumberSaveData saveData)
			:this(saveData.CurrentSeed)
		{
			for (int i = 0; i < saveData.CurrentNumberOfUses; i++)
				_randomNumberGenerator.Next();
		}

		public int GetRandomInteger()
		{
			return GetRandomInteger(0, int.MaxValue - 1);
		}

		// Note that the maxValue is included - unlike the built in random.
		public int GetRandomInteger(int minValue, int maxValue)
		{
			var number = _randomNumberGenerator.Next(minValue, maxValue + 1);
			_numberOfUses++;

			if (_numberOfUses == MAXNUMBEROFUSES)
			{
				_currentSeed = _randomNumberGenerator.Next();
				_randomNumberGenerator = new Random(_currentSeed);
			}

			return number;
		}

		// Note that this returns a minimum of 0.
		public int Dice(int num, int max, int add)
		{
			int total = add;

			for (int i = 0; i < num; i++)
				total += GetRandomInteger(1, max);

			return Math.Max(total, 0);
		}

		public int Dice(int num, int max)
		{
			return Dice(num, max, 0);
		}

		// Note that this is the traditional D20 - if a random number between 1 and 20 is needed use Dice or GetRandomInt
		public int D20(int add)
		{
			var roll = GetRandomInteger(1, 20);
			if (roll == 1)
				roll = -10;
			if (roll == 20)
				roll = 30;
			return roll + add;
		}

		public RandomNumberSaveData GetSaveData()
		{
			return new RandomNumberSaveData(_currentSeed, _numberOfUses);
		}
	}

	[Serializable]
	public struct RandomNumberSaveData
	{
		public int CurrentSeed;
		public int CurrentNumberOfUses;

		public RandomNumberSaveData(int currentSeed, int currentNumberOfUses)
		{
			CurrentSeed = currentSeed;
			CurrentNumberOfUses = currentNumberOfUses;
		}
	}
}
