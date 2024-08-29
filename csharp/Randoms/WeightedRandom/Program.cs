// See https://aka.ms/new-console-template for more information
using WeightedRandom;

Console.WriteLine("Hello, World!");

var itemWeights = new Dictionary<string, double>
{
	{ "A", 100 },
	{ "B", 80 },
	{ "C", 60 }
};
var selector = new WeightedRandomSelector<string>(itemWeights);
var selections = new Dictionary<string, int>();
int totalSelections = 1000000;

// Act
for (int i = 0; i < totalSelections; i++)
{
	string selected = selector.Select();
	if (!selections.ContainsKey(selected))
	{
		selections[selected] = 0;
	}
	selections[selected]++;
}

// Assert
double totalWeight = itemWeights.Values.Sum();
foreach (var item in itemWeights)
{
	double expectedProbability = item.Value / totalWeight;
	double actualProbability = (double)selections[item.Key] / totalSelections;
	var delta = Math.Abs(expectedProbability - actualProbability);
	Console.WriteLine($"Item {item.Key} was selected with probability {actualProbability}, " +
		$"expected {expectedProbability}, delta {delta}");
}
