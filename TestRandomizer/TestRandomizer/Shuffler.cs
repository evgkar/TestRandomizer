namespace TestRandomizer;

internal class Shuffler : IShuffler
{
    private readonly IRandomizer _randomizer;

    public Shuffler(IRandomizer randomizer)
    {
        _randomizer = randomizer;
    }

    public List<Item> Shuffle(List<Item> items)
    {
        throw new NotImplementedException();
    }
}
