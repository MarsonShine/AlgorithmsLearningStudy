using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedRandom
{
	internal class WeightedRandomSelector<T>(Dictionary<T, double> itemWeights)
		where T : notnull
	{
		private readonly Random _random = new Random();
		private readonly Dictionary<T, double> _itemWeights = itemWeights;

		public T Select()
		{
			double totalWeight = _itemWeights.Values.Sum();
			double randomWeight = _random.NextDouble() * totalWeight;
			double weightSum = 0;

			foreach (var item in _itemWeights)
			{
				weightSum += item.Value;
				if (randomWeight <= weightSum)
				{
					return item.Key;
				}
			}

			throw new InvalidOperationException("No item selected");
		}
	}
}
