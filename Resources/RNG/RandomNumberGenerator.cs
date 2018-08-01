using System;

namespace RLEngine.Resources.RNG
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

		int GetRandomInteger(int minValue, int maxValue)
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

		public int Dice(int num, int max, int add)
		{
			int total = add;

			for (int i = 0; i < num; i++)
				total += GetRandomInteger(1, max);

			return total;
		}

		public int Dice(int num, int max)
		{
			return Dice(num, max, 0);
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
