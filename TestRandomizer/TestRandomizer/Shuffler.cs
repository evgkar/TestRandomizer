namespace TestRandomizer;

internal class Shuffler : IShuffler
{
    private readonly IRandomizer _randomizer;

    public Shuffler(IRandomizer randomizer)
    {
        _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
    }

    public List<Item> Shuffle(List<Item> items)
    {
        List<Item> result = items
            .OrderBy(i => _randomizer.GetRandomValue())
            .ToList();

        int pretestElementsReplaced = 0;
        for (int i = 0; i < result.Count && pretestElementsReplaced < 2; i++)
        {
            if (result[i].ItemType == ItemTypeEnum.Pretest
                && i != pretestElementsReplaced)
            {
                var temp = result[i];
                result[i] = result[pretestElementsReplaced];
                result[pretestElementsReplaced++] = temp;
            }
        }

        return result;
    }
}
